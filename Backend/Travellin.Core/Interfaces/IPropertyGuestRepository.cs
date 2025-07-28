using Travellin.Core.Dtos;
using Travellin.Core.Dtos.PropertyGuests;
using Travellin.Core.Entities;
using Travellin.Travellin.Core.Shared;

namespace Travellin.Core.Interfaces
{
    public interface IPropertyGuestRepository
    {
        public Task<PaginatedResult<PropertyGuestDto>> GetByPropertyIdAsync(string propertyId, GetAllQueryDto dto);
        public Task<PropertyGuest?> GetByPropertyAndGuestTypeAsync(string propertyId, int guestTypeId);
        Task<List<PropertyGuest>> GetAllPropertyGuests(string propertyId);
        public void Create(PropertyGuest entity);
        public void Update(PropertyGuest entity);
        public void Delete(PropertyGuest entity);
    }
}
