using Travellin.Core.Entities;

namespace Travellin.Core.Interfaces
{
    public interface IAmenityRepository : IGenericRepository<Amenity, int>
    {
        public Task<bool> IsExistAsync(string propertyId, int amenityId);
    }
}
