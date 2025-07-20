using Travellin.Core.Dtos.PropertyAvailabilities;
using Travellin.Core.Entities;
using Travellin.Travellin.Core.Shared;
using System.Linq.Expressions;

namespace Travellin.Core.Interfaces
{
    public interface IPropertyAvailabilityRepository : IGenericRepository<PropertyAvailability, int>
    {
        public Task<List<PropertyAvailability>> GetAllAsync(Expression<Func<PropertyAvailability, bool>> filter);
        public Task<PaginatedResult<PropertyAvailabilityDto>> GetByPropertyIdAsync(string propertyId, PropertyAvailabilityQueryParamsDto queryDto);
        public void Delete(PropertyAvailability propertyAvailability);
    }
}
