using Travellin.Core.Dtos;
using Travellin.Core.Dtos.PropertySpaceItems;
using Travellin.Core.Entities;
using Travellin.Travellin.Core.Shared;

namespace Travellin.Core.Interfaces
{
    public interface IPropertySpaceItemRepository : IGenericRepository<PropertySpaceItem, int>
    {
        public Task<PaginatedResult<PropertySpaceItemDto>> GetByPropertySpaceIdAsync(string propertySpaceId, GetAllQueryDto dto);
    }
}
