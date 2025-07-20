using Travellin.Core.Dtos;
using Travellin.Core.Dtos.PropertyFees;
using Travellin.Core.Entities;
using Travellin.Travellin.Core.Shared;

namespace Travellin.Core.Interfaces
{
    public interface IPropertyFeeRepository : IGenericRepository<PropertyFee, int>
    {
        public Task<PaginatedResult<PropertyFeeDto>> GetByPropertyIdAsync(string propertyId, GetAllQueryDto dto);
    }
}
