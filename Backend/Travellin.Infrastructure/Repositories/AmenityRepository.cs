using Microsoft.EntityFrameworkCore;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Infrastructure.Data;

namespace Travellin.Infrastructure.Repositories
{
    class AmenityRepository : GenericRepository<Amenity, int>, IAmenityRepository
    {
        public AmenityRepository(TravellinDbContext dbContext) : base(dbContext)
        { }

        public async Task<bool> IsExistAsync(string propertyId, int amenityId)
        {
            var amenity = await _dbContext.PropertyAmenities.Where(x => x.PropertyId == propertyId && x.AmenityId == amenityId)
                .FirstOrDefaultAsync();

            return amenity is null ? false : true;
        }
    }
}
