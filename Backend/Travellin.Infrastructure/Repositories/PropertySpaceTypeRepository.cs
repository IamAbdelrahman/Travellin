using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Infrastructure.Data;

namespace Travellin.Infrastructure.Repositories
{
    class PropertySpaceTypeRepository : GenericRepository<PropertySpaceType, int>, IPropertySpaceTypeRepository
    {
        public PropertySpaceTypeRepository(TravellinDbContext dbContext) : base(dbContext)
        { }
    }
}
