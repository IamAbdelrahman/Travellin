using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Infrastructure.Data;

namespace Travellin.Infrastructure.Repositories
{
    class RegionRepository : GenericRepository<Region, int>, IRegionRepository
    {
        public RegionRepository(TravellinDbContext dbContext) : base(dbContext) { }
    }
}
