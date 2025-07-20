using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Infrastructure.Data;

namespace Travellin.Infrastructure.Repositories
{
    class PropertySpaceItemTypeRepository : GenericRepository<PropertySpaceItemType, int>, IPropertySpaceItemTypeRepository
    {
        public PropertySpaceItemTypeRepository(TravellinDbContext dbContext) : base(dbContext)
        { }
    }
}
