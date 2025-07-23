using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.Models;
using Stripe.Identity;
using System.Threading.Tasks;
using Travellin.Core.Dtos;
using Travellin.Core.Dtos.Accounts;
using Travellin.Core.Dtos.Properties;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Core.Mappings;
using Travellin.Core.Mappings;
using Travellin.Infrastructure.Data;
using Travellin.Travellin.Core.Enums;
using Travellin.Travellin.Core.Shared;
namespace Travellin.Infrastructure.Repositories
{
    class PropertyRepository : GenericRepository<Property, string>, IPropertyRepository
    {
        public PropertyRepository(TravellinDbContext dbContext) : base(dbContext)
        { }

        public async Task<PaginatedResult<PropertyListItemDto>> GetFilteredPropertiesAsync(FilterPropertyQueryParamsDto queryDto, LoggedInUser? currUser)
        {
            var isAdmin = currUser?.IsInRole("Admin") ?? false;
            var isHost = currUser?.IsInRole("Host") ?? false;

            var query = _dbContext.Properties
                    .Include(x => x.Owner)
                    .Include(x => x.Location)
                    .ThenInclude(x => x.Country)
                    .Include(x => x.PropertyType)
                    .Include(x => x.PropertyPhotos)
                    .ThenInclude(x => x.FileUpload)
                    .Include(x => x.Bookings)
                    .ThenInclude(x => x.Review)
                    .AsQueryable();

            if (isAdmin)
            {
                if (queryDto.IsActive.HasValue)
                    query = query.Where(x => x.IsActive == queryDto.IsActive);

                if (queryDto.IsDeleted.HasValue)
                    query = query.Where(x => x.IsDeleted == queryDto.IsDeleted);
            }
            else if (isHost)
            {
                query = query.Where(x => !x.IsDeleted && (x.IsActive || (!x.IsActive && x.OwnerId == currUser.Id)));

                if (queryDto.IsActive.HasValue && queryDto.IsActive == false)
                {
                    query = query.Where(x => x.OwnerId == currUser.Id && !x.IsActive);
                }
            }
            else
            {
                query = query.Where(x => x.IsActive && !x.IsDeleted);
            }

            // Fitlers
            if (!string.IsNullOrEmpty(queryDto.OwnerId))
            {
                query = query.Where(x => x.OwnerId == queryDto.OwnerId);
            }

            if (queryDto.LocationId.HasValue)
            {
                query = query.Where(x => x.LocationId == queryDto.LocationId.Value);
            }
            else if (!string.IsNullOrEmpty(queryDto.LocationName))
            {
                query = query.Where(x => EF.Functions.Like(x.Location.Name, $"%{queryDto.LocationName}%"));
            }

            if (queryDto.PropertyTypeId.HasValue)
                query = query.Where(x => x.PropertyTypeId == queryDto.PropertyTypeId.Value);

            if (queryDto.GuestCount.HasValue)
                query = query.Where(x => x.PropertyGuests.Sum(pg => pg.GuestCount) >= queryDto.GuestCount.Value);

            if (queryDto.PriceMin.HasValue)
                query = query.Where(x => x.PricePerNight >= queryDto.PriceMin.Value);

            if (queryDto.PriceMax.HasValue)
                query = query.Where(x => x.PricePerNight <= queryDto.PriceMax.Value);


            if (queryDto.CheckIn.HasValue && queryDto.CheckIn.Value >= DateOnly.FromDateTime(DateTime.Today))
            {
                var checkInDate = queryDto.CheckIn.Value.ToDateTime(TimeOnly.MinValue);

                if (queryDto.CheckOut.HasValue)
                {
                    var checkOutDate = queryDto.CheckOut.Value.ToDateTime(TimeOnly.MinValue);

                    query = query.Where(x =>
                        x.PropertyAvailabilities.Any(pa =>
                            pa.IsAvailable &&
                            pa.StartDate <= checkOutDate &&
                            pa.EndDate >= checkInDate
                        ) &&
                        !x.Bookings.Any(b =>
                            b.Status != BookingStatus.Cancelled &&
                            b.CheckIn < checkOutDate &&
                            b.CheckOut > checkInDate
                        )
                    );
                }
                else
                {
                    query = query.Where(x =>
                        x.PropertyAvailabilities.Any(pa =>
                            pa.IsAvailable &&
                            pa.StartDate <= checkInDate &&
                            pa.EndDate >= checkInDate
                        ) &&
                        !x.Bookings.Any(b =>
                            b.Status != BookingStatus.Cancelled &&
                            b.CheckIn <= checkInDate &&
                            b.CheckOut > checkInDate
                        )
                    );
                }
            }

            if (queryDto.CountryId.HasValue)
                query = query.Where(x => x.Location.CountryId == queryDto.CountryId.Value);
            else if (!string.IsNullOrEmpty(queryDto.CountryName))
                query = query.Where(x => EF.Functions.Like(x.Location.Country.Name, $"%{queryDto.CountryName}%"));

            if (queryDto.RegionId.HasValue)
                query = query.Where(x => x.Location.Country.RegionId == queryDto.RegionId.Value);

            // Add average rating filtering
            if (queryDto.MinAvgRating.HasValue || queryDto.MaxAvgRating.HasValue)
            {
                query = query.Where(x => x.Bookings
                    .Where(b => b.Review != null)
                    .Any()); // Ensure property has at least one review

                if (queryDto.MinAvgRating.HasValue)
                {
                    query = query.Where(x => x.Bookings
                        .Where(b => b.Review != null)
                        .Average(b => (b.Review.Cleanliness + b.Review.Accuracy + b.Review.CheckIn +
                                     b.Review.Communication + b.Review.Location + b.Review.Value) / 6m)
                        >= queryDto.MinAvgRating.Value);
                }

                if (queryDto.MaxAvgRating.HasValue)
                {
                    query = query.Where(x => x.Bookings
                        .Where(b => b.Review != null)
                        .Average(b => (b.Review.Cleanliness + b.Review.Accuracy + b.Review.CheckIn +
                                     b.Review.Communication + b.Review.Location + b.Review.Value) / 6m)
                        <= queryDto.MaxAvgRating.Value);
                }
            }

            query = queryDto.Sort switch
            {
                "price_asc" => query.OrderByDescending(x => x.PricePerNight),
                "price_dec" => query.OrderBy(x => x.PricePerNight),
                _ => query.OrderByDescending(x =>
                    x.Bookings
                     .Where(b => b.Review != null)
                     .Average(b =>
                         (b.Review.Cleanliness + b.Review.Accuracy + b.Review.CheckIn +
                          b.Review.Communication + b.Review.Location + b.Review.Value) / 6m)
                )
            };

            // Pagination
            var total = await query.CountAsync();
            var items = await query.Skip(queryDto.CalcSkippedItems()).Take(queryDto.PageSize).Select(p => p.ToPropertyListItemDto()).ToListAsync();

            return new()
            {
                Items = items,
                MetaData = new PaginationMetaData
                {
                    Total = total,
                    Page = queryDto.Page,
                    PageSize = queryDto.PageSize
                }
            };
        }

        public async Task<PropertyDetailsDto?> GetPropertyDetailsAsync(string id, LoggedInUser? currUser)
        {
            var isAdmin = currUser?.IsInRole("Admin") ?? false;
            var isHost = currUser?.IsInRole("Host") ?? false;

            var query = _dbContext.Properties
                .Include(x => x.Owner)
                .Include(x => x.Location)
                .Include(x => x.PropertyType)
                .Include(x => x.PropertyPhotos)
                .ThenInclude(x => x.FileUpload)
                .Include(x => x.Bookings)
                .ThenInclude(x => x.Review)
                .Include(x => x.PropertyGuests)
                .Include(x => x.PropertySpaces)
                .ThenInclude(x => x.PropertySpaceType)
                .Include(x => x.PropertySpaces)
                .ThenInclude(x => x.PropertySpaceItems)
                .ThenInclude(x => x.PropertySpaceItemType)
                .Where(x => x.Id == id)
                .AsQueryable();

            if (isHost)
            {
                query = query.Where(x => !x.IsDeleted);
            }
            else if (!isAdmin)
            {
                query = query.Where(x => x.IsActive && !x.IsDeleted);
            }


            var property = await query.Select(p =>
                p.ToPropertyDetailsDto()
            ).FirstOrDefaultAsync();

            return property;
        }
        public async void FromUpdateDtoToEntity(Property entity, PropertyUpdateDto dto)
        {
            PropertyMappingExtensions.ToEntity(entity, dto);
        }

        public override Property GetById(string id)
        {
            return _dbContext.Properties.Find(id) ?? throw new Exception("Not found");
        }
        ////public async Task DeleteAsync(Property entity)
        ////{
        ////    _dbContext.Properties.Remove(entity);
        ////     await Task.CompletedTask;

        //}
        public override async Task<List<Property>> GetAll()
        {
            return await _dbContext.Properties.ToListAsync();
        }

        public void FromCreateEntityToDto(PropertyCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Property property)
        {
            var photos = _dbContext.PropertyPhotos.Where(p => p.PropertyId == property.Id);
            _dbContext.PropertyPhotos.RemoveRange(photos);

            var guests = _dbContext.PropertyGuests.Where(p => p.PropertyId == property.Id);
            _dbContext.PropertyGuests.RemoveRange(guests);

            var fees = _dbContext.PropertyFees.Where(p => p.PropertyId == property.Id);
            _dbContext.PropertyFees.RemoveRange(fees);

            var availabilities = _dbContext.PropertyAvailabilities.Where(p => p.PropertyId == property.Id);
            _dbContext.PropertyAvailabilities.RemoveRange(availabilities);

            var bookings = _dbContext.Bookings.Where(b => b.PropertyId == property.Id);
            _dbContext.Bookings.RemoveRange(bookings);

            var propertySpaces = _dbContext.PropertySpaces
                .Where(p => p.PropertyId == property.Id)
                .ToList();

            var spaceIds = propertySpaces.Select(s => s.Id).ToList();

            var spaceItems = _dbContext.PropertySpaceItems
                .Where(i => spaceIds.Contains(i.PropertySpaceId));
            _dbContext.PropertySpaceItems.RemoveRange(spaceItems);

            _dbContext.PropertySpaces.RemoveRange(propertySpaces);

            var amenities = _dbContext.PropertyAmenities.Where(p => p.PropertyId == property.Id);
            _dbContext.PropertyAmenities.RemoveRange(amenities);

            _dbContext.Properties.Remove(property);

            await Task.CompletedTask;
        }
    }
        
    
}
