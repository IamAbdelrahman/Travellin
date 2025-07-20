using Travellin.Core.Dtos.BookingGuests;
using Travellin.Core.Dtos.Bookings;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Travellin.Core.Shared;
using Travellin.Travellin.Core.Enums;

namespace Travellin.Infrastructure.Services
{
    class BookingManagementService : IBookingManagementService
    {
        private IUnitOfWork _unitOfWork;
        public BookingManagementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Booking> CreateBookingAsync(string userId, CreateBookingDto dto)
        {
            var property = await _unitOfWork.PropertyRepository.GetByIdAsync(
                dto.PropertyId,
                x => x.PropertyFees,
                x => x.PropertyAvailabilities,
                x => x.PropertyGuests);

            if (property is null)
                throw new NotFoundException($"Property with id [{dto.PropertyId}] not found");


            // Validate guest counts against property limits
            ValidateGuestCounts(property, dto.Guests);

            var isAvailable = IsPropertyAvailable(property, dto.CheckIn, dto.Checkout);
            if (!isAvailable)
            {
                throw new ConflictException("Property is not available for the selected dates");
            }

            await UpdateAvailabilityRecordsAsync(property, dto.CheckIn, dto.Checkout);

            var booking = new Booking
            {
                PropertyId = dto.PropertyId,
                UserId = userId,
                CheckIn = dto.CheckIn,
                CheckOut = dto.Checkout,
                PricePerNight = property.PricePerNight,
                TotalFees = CalcTotalFees(property),
                Status = BookingStatus.Pending,
                BookingGuests = dto.Guests.Select(g => new BookingGuest
                {
                    GuestTypeId = g.GuestTypeId,
                    GuestCount = g.GuestCount
                }).ToList()
            };

            _unitOfWork.BookingRepository.Create(booking);
            await _unitOfWork.SaveChangesAsync();

            return booking;
        }

        public async Task CancelBookingAsync(string bookingId, string userId, bool isAdmin)
        {
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(bookingId, x => x.Property);

            if (booking == null)
            {
                throw new NotFoundException($"Booking with id [{bookingId}] not found");
            }

            // Authorization check
            if (!isAdmin && booking.UserId != userId)
            {
                throw new UnauthorizedException("You can only cancel your own bookings");
            }

            // Status validation
            if (booking.Status != BookingStatus.Pending)
            {
                throw new ConflictException("Only pending bookings can be canceled");
            }

            // Update booking status
            booking.Status = BookingStatus.Cancelled;
            booking.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.BookingRepository.Update(booking);

            // Restore availability
            await RestoreAvailabilityAsync(booking.Property, booking.CheckIn, booking.CheckOut);

            await _unitOfWork.SaveChangesAsync();
        }


        private void ValidateGuestCounts(Property property, List<CreateBookingGuestDto> guests)
        {
            if (property.PropertyGuests == null || !property.PropertyGuests.Any())
                return;

            foreach (var guestDto in guests)
            {
                var propertyGuest = property.PropertyGuests
                    .FirstOrDefault(pg => pg.GuestTypeId == guestDto.GuestTypeId);

                if (propertyGuest == null)
                {
                    throw new ConflictException($"Guest type {guestDto.GuestTypeId} is not allowed for this property");
                }

                if (guestDto.GuestCount > propertyGuest.GuestCount)
                {
                    throw new ConflictException(
                        $"Maximum {propertyGuest.GuestCount} guests of type {propertyGuest.GuestType?.Name} allowed, but {guestDto.GuestCount} requested");
                }
            }
        }

        private bool IsPropertyAvailable(Property property, DateTime checkIn, DateTime checkOut)
        {
            var overlappingAvailabilities = property.PropertyAvailabilities
                .Where(a => a.IsAvailable &&
                           a.StartDate < checkOut &&
                           a.EndDate > checkIn)
                .OrderBy(a => a.StartDate)
                .ToList();

            if (!overlappingAvailabilities.Any())
                return false;

            // Check for contiguous coverage
            DateTime currentCoverage = checkIn;

            foreach (var availability in overlappingAvailabilities)
            {
                if (availability.StartDate > currentCoverage)
                    return false; // Gap found

                if (availability.EndDate > currentCoverage)
                    currentCoverage = availability.EndDate;

                if (currentCoverage >= checkOut)
                    return true;
            }

            return currentCoverage >= checkOut;
        }

        private decimal CalcTotalFees(Property property)
        {
            if (property is null || property.PropertyFees is null) return 0;
            return property.PropertyFees.Sum(x => x.Amount);
        }

        private async Task UpdateAvailabilityRecordsAsync(Property property, DateTime checkIn, DateTime checkOut)
        {
            // Get all AVAILABLE availability records that overlap with the booking dates
            var overlappingAvailabilities = property.PropertyAvailabilities
                .Where(a => a.StartDate <= checkOut && a.EndDate >= checkIn && a.IsAvailable)
                .OrderBy(a => a.StartDate)
                .ToList();

            foreach (var availability in overlappingAvailabilities)
            {
                // Case 1: Availability record is completely within the booking period
                if (availability.StartDate >= checkIn && availability.EndDate <= checkOut)
                {
                    // Mark entire record as unavailable
                    availability.IsAvailable = false;
                    _unitOfWork.PropertyAvailabilityRepository.Update(availability);
                    continue;
                }

                // Case 2: Booking starts within this availability period
                if (availability.StartDate < checkIn && availability.EndDate > checkIn)
                {
                    // Split into available (before) and unavailable (during) parts
                    var before = new PropertyAvailability
                    {
                        PropertyId = property.Id,
                        StartDate = availability.StartDate,
                        EndDate = checkIn,
                        IsAvailable = true
                    };

                    var during = new PropertyAvailability
                    {
                        PropertyId = property.Id,
                        StartDate = checkIn,
                        EndDate = availability.EndDate.Min(checkOut),
                        IsAvailable = false
                    };

                    _unitOfWork.PropertyAvailabilityRepository.Create(before);
                    _unitOfWork.PropertyAvailabilityRepository.Create(during);
                    await _unitOfWork.PropertyAvailabilityRepository.DeleteAsync(availability.Id);
                    continue;
                }

                // Case 3: Booking ends within this availability period
                if (availability.StartDate < checkOut && availability.EndDate > checkOut)
                {
                    // Split into unavailable (during) and available (after) parts
                    var during = new PropertyAvailability
                    {
                        PropertyId = property.Id,
                        StartDate = availability.StartDate.Max(checkIn),
                        EndDate = checkOut,
                        IsAvailable = false
                    };

                    var after = new PropertyAvailability
                    {
                        PropertyId = property.Id,
                        StartDate = checkOut,
                        EndDate = availability.EndDate,
                        IsAvailable = true
                    };

                    _unitOfWork.PropertyAvailabilityRepository.Create(during);
                    _unitOfWork.PropertyAvailabilityRepository.Create(after);
                    await _unitOfWork.PropertyAvailabilityRepository.DeleteAsync(availability.Id);
                }
            }
        }


        private async Task RestoreAvailabilityAsync(Property property, DateTime checkIn, DateTime checkOut)
        {
            // Get all UNAVAILABLE availability records that overlap with the booking dates
            var overlappingAvailabilities = await _unitOfWork.PropertyAvailabilityRepository
                .GetAllAsync(a => a.PropertyId == property.Id &&
                                 !a.IsAvailable &&
                                 a.StartDate < checkOut &&
                                 a.EndDate > checkIn);

            foreach (var availability in overlappingAvailabilities)
            {
                // Case 1: Record exactly matches booking period
                if (availability.StartDate == checkIn && availability.EndDate == checkOut)
                {
                    availability.IsAvailable = true;
                    _unitOfWork.PropertyAvailabilityRepository.Update(availability);
                    continue;
                }

                // Find adjacent records that might need merging
                var previous = await _unitOfWork.PropertyAvailabilityRepository
                    .GetAllAsync(a => a.PropertyId == property.Id &&
                                    a.EndDate == availability.StartDate);

                var next = await _unitOfWork.PropertyAvailabilityRepository
                    .GetAllAsync(a => a.PropertyId == property.Id &&
                                    a.StartDate == availability.EndDate);

                // Check if we can merge with adjacent available periods
                var prevAvailable = previous.FirstOrDefault()?.IsAvailable == true;
                var nextAvailable = next.FirstOrDefault()?.IsAvailable == true;

                if (prevAvailable && nextAvailable)
                {
                    // Merge all three periods
                    var prevRecord = previous.First();
                    var nextRecord = next.First();

                    prevRecord.EndDate = nextRecord.EndDate;
                    _unitOfWork.PropertyAvailabilityRepository.Update(prevRecord);

                    _unitOfWork.PropertyAvailabilityRepository.Delete(availability);
                    await _unitOfWork.PropertyAvailabilityRepository.DeleteAsync(nextRecord.Id);
                }
                else if (prevAvailable)
                {
                    // Merge with previous
                    var prevRecord = previous.First();
                    prevRecord.EndDate = availability.EndDate;
                    _unitOfWork.PropertyAvailabilityRepository.Update(prevRecord);

                    _unitOfWork.PropertyAvailabilityRepository.Delete(availability);
                }
                else if (nextAvailable)
                {
                    // Merge with next
                    var nextRecord = next.First();
                    nextRecord.StartDate = availability.StartDate;
                    _unitOfWork.PropertyAvailabilityRepository.Update(nextRecord);

                    _unitOfWork.PropertyAvailabilityRepository.Delete(availability);
                }
                else
                {
                    // Just mark as available
                    availability.IsAvailable = true;
                    _unitOfWork.PropertyAvailabilityRepository.Update(availability);
                }
            }
        }
    }
}
