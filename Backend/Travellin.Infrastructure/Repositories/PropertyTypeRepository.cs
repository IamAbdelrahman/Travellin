using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Infrastructure.Data;

namespace Travellin.Infrastructure.Repositories
{
    class PropertyTypeRepository : GenericRepository<PropertyType, int>, IPropertyTypeRepository
    {
        public PropertyTypeRepository(TravellinDbContext dbContext) : base(dbContext)
        { }
    }
}
