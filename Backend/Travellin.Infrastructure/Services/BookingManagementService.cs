using Travellin.Core.Dtos.BookingGuests;
using Travellin.Core.Dtos.Bookings;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Travellin.Core.Shared;
using Travellin.Travellin.Core.Enums;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Travellin.Infrastructure.Services
{
    class BookingManagementService : IBookingManagementService
    {
        private IUnitOfWork _unitOfWork;
        public BookingManagementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //////////////////////////////////Create Booking////////////////////////////////////
        public async Task<Booking> CreateBookingAsync(string userId, CreateBookingDto dto)
        {
           
            //Fetch Property user select
            var property = await _unitOfWork.PropertyRepository.GetByIdAsync(dto.PropertyId);

            if (property is null)
                throw new NotFoundException($"Property with id [{dto.PropertyId}] not found");


            // Validate guest counts 
            await ValidateGuestCounts(property, dto.Guests);

            //Check Poroperty is available for the selected dates
            var isAvailable = await IsPropertyAvailable(property, dto.CheckIn, dto.Checkout);
            if (!isAvailable)
            {
                throw new ConflictException("Property is not available for the selected dates");
            }


            ////////////////////////////Calculate Total Fees////////////////////////////////////
            var propertyFees = await _unitOfWork.PropertyFeeRepository.GetAllByPropertyIdAsync(dto.PropertyId);
            var totalFees = propertyFees.Sum(f => f.Amount);

            //If yes and user reserve those nights mark as unavailable
            //Update:those nights block them as they are booked now so any coming guest cannot book them
            await UpdateAvailabilityRecordsAsync(property, dto.CheckIn, dto.Checkout);

            //Create booking
            var booking = new Booking
            {
                PropertyId = dto.PropertyId,
                UserId = userId,
                CheckIn = dto.CheckIn,
                CheckOut = dto.Checkout,
                PricePerNight = property.PricePerNight,
                TotalFees = totalFees,
                Status = BookingStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
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


        //Cancel Booking
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

        //////////////////////////////////Validate Guest Count and Type////////////////////////////////////
        private async Task ValidateGuestCounts(Property property, List<CreateBookingGuestDto> guests)
        {
            var propertyGuests = await _unitOfWork.PropertyGuestRepository.GetAllPropertyGuests(property.Id);

            if (property.PropertyGuests == null || !property.PropertyGuests.Any())
                return;

            foreach (var guestDto in guests)
            {
                var propertyGuest = propertyGuests
                    .FirstOrDefault(pg => pg.GuestTypeId == guestDto.GuestTypeId);

                if (propertyGuest == null)
                {
                    // Get the name of the rejected guest type
                    var guestType = await _unitOfWork.GuestTypeReposiotry.GetByIdAsync(guestDto.GuestTypeId);
                    var guestTypeName = guestType?.Name ?? $"ID [{guestDto.GuestTypeId}]";

                    // Build list of allowed guest type names
                    var allowedGuestTypeNames = propertyGuests
                        .Where(pg => pg.GuestType != null)
                        .Select(pg => $"'{pg.GuestType.Name}'")
                        .ToList();

                    var allowedListStr = allowedGuestTypeNames.Any()
                        ? string.Join(", ", allowedGuestTypeNames)
                        : "none";

                    throw new ConflictException(
                        $"Guest type '{guestTypeName}' is not allowed for this property. Allowed guest types: {allowedListStr}");
                }

                if (guestDto.GuestCount > propertyGuest.GuestCount)
                {
                    throw new ConflictException(
                        $"Maximum '{propertyGuest.GuestCount}' guests of type '{propertyGuest.GuestType?.Name}' allowed, but '{guestDto.GuestCount}' exceeded");
                }
            }
        }
        /////////////////////////////////////Check Property Availability////////////////////////////////////
        private async Task<bool> IsPropertyAvailable(Property property, DateTime checkIn, DateTime checkOut)
        {
            var CheckAvailable = await _unitOfWork.PropertyAvailabilityRepository.GetAllAsync(a => a.PropertyId == property.Id &&
                                  a.IsAvailable &&
                                  a.StartDate < checkOut &&
                                  a.EndDate > checkIn);

            // 2. If no available periods exist at all — not available
            if (!CheckAvailable.Any())
                return false;

            // 3. Check that the availability windows fully cover the desired range without gaps
            DateTime currentCoverage = checkIn;

            foreach (var availability in CheckAvailable)
            {
                if (availability.StartDate > currentCoverage)
                    return false; // ⛔ Gap found — property not available for full duration

                if (availability.EndDate > currentCoverage)
                    currentCoverage = availability.EndDate;

                if (currentCoverage >= checkOut)
                    return true; // ✅ Full coverage confirmed
            }

            return currentCoverage >= checkOut; // Check final coverage in case last range reaches end

        }

        //////////////////////////////////Update Dates////////////////////////////////////
        private async Task UpdateAvailabilityRecordsAsync(Property property, DateTime checkIn, DateTime checkOut)
        {
            //Here assume guest choose property 
            //Property may be available during multiple date periods.
            //We need to find all availability records that overlap with the booking dates
            //Available 1-->11 and books 5-->10 (Available)
            //Anoother Guest books 4-->8 (Unavailable) but 1-->5  is available as mean 1,2,3,4 and 5 checkout excluded and 10 -->11 is available (overlaps)
            var overlappingAvailabilities = property.PropertyAvailabilities
                .Where(a => a.StartDate <= checkOut && a.EndDate >= checkIn && a.IsAvailable)
                .OrderBy(a => a.StartDate)
                .ToList();
            var date = property.PropertyAvailabilities.Select(a => a.StartDate).ToList();
            //Anoother Guest books 4-->8 (Unavailable) but 1-->5 is available and 10 -->11 is available (overlaps)
            //Loop through each overlapping availability
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

        //Restore
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

        ////////////////////////////////////////Host to accept or decline booking////////////////////////////////////
        public async Task AcceptBookingAsync(string bookingId)
        {
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(bookingId);
            if (booking == null)
                throw new NotFoundException($"Booking {bookingId} not found.");

            if (booking.Status != BookingStatus.Pending)
                throw new ConflictException("Only pending bookings can be accepted.");

            booking.Status = BookingStatus.Confirmed;
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeclineBookingAsync(string bookingId)
        {
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(bookingId);
            if (booking == null)
                throw new NotFoundException($"Booking {bookingId} not found.");

            if (booking.Status != BookingStatus.Pending)
                throw new ConflictException("Only pending bookings can be declined.");

            booking.Status = BookingStatus.Declined;
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
