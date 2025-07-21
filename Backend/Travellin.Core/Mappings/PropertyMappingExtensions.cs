using Travellin.Core.Dtos.Properties;
using Travellin.Core.Dtos.PropertySpaceItemTypes;
using Travellin.Core.Dtos.PropertySpaceTypes;
using Travellin.Core.Dtos.UserProfilesDto;
using Travellin.Core.Entities;

namespace Travellin.Core.Mappings
{
    public static class PropertyMappingExtensions
    {
        public static PropertyDto ToDto(this Property entity)
        {
            return new PropertyDto
            {
                Id = entity.Id,
                Title = entity.Title,
                OwnerId = entity.OwnerId,
                PropertyTypeId = entity.PropertyTypeId,
                LocationId = entity.LocationId,
                PricePerNight = entity.PricePerNight,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                SafteyInfo = entity.SafteyInfo,
                HouseRules = entity.HouseRules,
                CancellationPolicy = entity.CancellationPolicy,
                IsActive = entity.IsActive,
                IsDeleted = entity.IsDeleted
            };
        }

        public static PropertyListItemDto ToPropertyListItemDto(this Property property)
        {
            var (averageRating, reviewCount) = CalculateRatingStats(property);

            return new PropertyListItemDto
            {
                Id = property.Id,
                Title = property.Title,
                PricePerNight = property.PricePerNight,
                Latitude = property.Latitude,
                Longitude = property.Longitude,
                Owner = property.Owner.ToDto(),
                Location = property.Location.ToDto(),
                PropertyType = property.PropertyType.ToDto(),
                Photos = property.PropertyPhotos.OrderBy(x => x.TouchedAt).Select(photo => photo.ToDto()).ToList(),
                AverageRating = averageRating,
                ReviewCount = reviewCount,
                IsActive = property.IsActive,
                IsDeleted = property.IsDeleted
            };
        }

        public static PropertyDetailsDto ToPropertyDetailsDto(this Property property)
        {
            var (averageRating, reviewCount) = CalculateRatingStats(property);

            return new PropertyDetailsDto
            {
                Id = property.Id,
                Title = property.Title,
                Description = property.Description,
                PricePerNight = property.PricePerNight,
                Latitude = property.Latitude,
                Longitude = property.Longitude,
                SafteyInfo = property.SafteyInfo,
                HouseRules = property.HouseRules,
                CancellationPolicy = property.CancellationPolicy,
                Owner = property.Owner.ToDto(),
                Location = property.Location.ToDto(),
                PropertyType = property.PropertyType.ToDto(),
                Photos = property.PropertyPhotos.OrderBy(x => x.TouchedAt).Select(photo => photo.ToDto()).ToList(),
                AverageRating = averageRating,
                ReviewCount = reviewCount,
                MaxGuestCount = MapMaxGuestCount(property),
                SpaceSummaries = MapSpaceSummaries(property),
                SpaceItemSummaries = MapSpaceItemSummaries(property),
                IsActive = property.IsActive,
                IsDeleted = property.IsDeleted
            };
        }

        public static void ToEntity (this Property propertyEntity, PropertyUpdateDto dto)
        {
            propertyEntity.Latitude = dto.Latitude ?? propertyEntity.Latitude;
            propertyEntity.Title = dto.Title ?? propertyEntity.Title;
            propertyEntity.Description = dto.Description ?? propertyEntity.Description;
            propertyEntity.PropertyTypeId = dto.PropertyTypeId ?? propertyEntity.PropertyTypeId;
            propertyEntity.PricePerNight = dto.PricePerNight ?? propertyEntity.PricePerNight;
            propertyEntity.Longitude = dto.Longitude ?? propertyEntity.Longitude;
            propertyEntity.LocationId = dto.LocationId ?? propertyEntity.LocationId;
            propertyEntity.SafteyInfo = dto.SafteyInfo ?? propertyEntity.SafteyInfo;
            propertyEntity.HouseRules = dto.HouseRules ?? propertyEntity.HouseRules;
            propertyEntity.CancellationPolicy = dto.CancellationPolicy ?? propertyEntity.CancellationPolicy;
            propertyEntity.IsActive = dto.IsActive ?? propertyEntity.IsActive;
        }
        private static (decimal averageRating, int reviewCount) CalculateRatingStats(Property property)
        {
            var reviews = property.Bookings?
                .Where(b => b.Review != null)
                .Select(b => b.Review)
                .ToList() ?? new List<Review>();

            var reviewCount = reviews.Count;
            var averageRating = reviewCount > 0
                ? reviews.Average(r => (r.Cleanliness + r.Accuracy + r.CheckIn +
                                      r.Communication + r.Location + r.Value) / 6)
                : 0;

            return (averageRating, reviewCount);
        }

        private static List<PropertySpaceSummaryDto> MapSpaceSummaries(Property property)
        {
            return property.PropertySpaces?
                .GroupBy(ps => new
                {
                    ps.PropertySpaceTypeId,
                    ps.PropertySpaceType.Name,
                    ps.IsShared
                })
                .Select(g => new PropertySpaceSummaryDto
                {
                    SpaceType = new PropertySpaceTypeDto
                    {
                        Id = g.Key.PropertySpaceTypeId,
                        Name = g.Key.Name
                    },
                    Count = g.Count(),
                    IsShared = g.Key.IsShared
                })
                .ToList() ?? new List<PropertySpaceSummaryDto>();
        }

        private static List<PropertySpaceItemSummaryDto> MapSpaceItemSummaries(Property property)
        {
            return property?.PropertySpaces?
                .SelectMany(space => space.PropertySpaceItems)
                .GroupBy(item => item.PropertySpaceItemType)
                .Select(g => new PropertySpaceItemSummaryDto
                {
                    ItemType = new PropertySpaceItemTypeDto
                    {
                        Id = g.First().PropertySpaceItemType.Id,
                        Name = g.First().PropertySpaceItemType.Name
                    },
                    Quantity = g.Sum(item => item.Quantity)
                })
                .ToList() ?? new List<PropertySpaceItemSummaryDto>();
        }

        private static int MapMaxGuestCount(Property property)
        {
            return property?.PropertyGuests.Sum(x => x.GuestCount) ?? 0;
        }
    }
}
