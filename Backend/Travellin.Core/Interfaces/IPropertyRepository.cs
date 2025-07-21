using Travellin.Core.Dtos.Accounts;
using Travellin.Core.Dtos.Properties;
using Travellin.Core.Entities;
using Travellin.Travellin.Core.Shared;

namespace Travellin.Core.Interfaces
{
    public interface IPropertyRepository : IGenericRepository<Property, string>
    {
        public Task<PaginatedResult<PropertyListItemDto>> GetFilteredPropertiesAsync(FilterPropertyQueryParamsDto queryDto, LoggedInUser? currUser = null);
        public Task<PropertyDetailsDto?> GetPropertyDetailsAsync(string id, LoggedInUser? currUser = null);
        public void FromUpdateDtoToEntity(Property entity, PropertyUpdateDto dto);
        public void FromCreateEntityToDto(PropertyCreateDto dto);
        Task DeleteAsync(Property property);
    }

}
