using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Infrastructure.Data;

namespace Travellin.Infrastructure.Repositories
{
    class AmenityCategoryRepository : GenericRepository<AmenityCategory, int>, IAmenityCategoryRepository
    {
        public AmenityCategoryRepository(TravellinDbContext dbContext) : base(dbContext)
        {
        }
    }
}
