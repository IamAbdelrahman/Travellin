using Travellin.Core.Dtos;
using Travellin.Core.Dtos.PropertyAmenities;
using Travellin.Core.Entities;
using Travellin.Travellin.Core.Shared;

namespace Travellin.Core.Interfaces
{
    public interface IPropertyAmenityRepository
    {
        public Task<PaginatedResult<PropertyAmenityDto>> GetByPropertyIdAsync(string propertyId, GetAllQueryDto dto);
        public Task<PropertyAmenity?> GetPropertyAmenityAsync(string propertyId, int amenityId);
        public void Create(PropertyAmenity entity);
        public void Delete(PropertyAmenity entity);
        public Task DeleteAsync(string propertyId, int amenityId);
    }
}
