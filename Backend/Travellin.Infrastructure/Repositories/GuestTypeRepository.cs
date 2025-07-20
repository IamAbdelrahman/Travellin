using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Infrastructure.Data;

namespace Travellin.Infrastructure.Repositories
{
    class GuestTypeRepository : GenericRepository<GuestType, int>, IGuestTypeReposiotry
    {
        public GuestTypeRepository(TravellinDbContext dbContext) : base(dbContext)
        { }
    }
}
