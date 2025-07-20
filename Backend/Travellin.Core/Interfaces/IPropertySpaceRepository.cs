using Travellin.Core.Dtos;
using Travellin.Core.Dtos.PropertySpaces;
using Travellin.Core.Entities;
using Travellin.Travellin.Core.Shared;

namespace Travellin.Core.Interfaces
{
    public interface IPropertySpaceRepository : IGenericRepository<PropertySpace, string>
    {
        public Task<PaginatedResult<PropertySpaceDto>> GetByPropertyIdAsync(string propertyId, GetAllQueryDto dto);
    }
}
