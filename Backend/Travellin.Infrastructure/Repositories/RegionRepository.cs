using Travellin.Core.Dtos.Regions;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Core.Mappings;
using Travellin.Infrastructure.Data;

namespace Travellin.Infrastructure.Repositories
{
    class RegionRepository : GenericRepository<Region, int>, IRegionRepository
    {
        public RegionRepository(TravellinDbContext dbContext) : base(dbContext) { }
        public RegionDto ToRegionDto(Region region)
        {
            return region.ToDto();
        }

    }
}
