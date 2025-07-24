using Travellin.Core.Dtos.PropertyTypes;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Core.Mappings;
using Travellin.Infrastructure.Data;

namespace Travellin.Infrastructure.Repositories
{
    class PropertyTypeRepository : GenericRepository<PropertyType, int>, IPropertyTypeRepository
    {
        public PropertyTypeRepository(TravellinDbContext dbContext) : base(dbContext)
        { }
        public PropertyTypeDto FromEntityToDto (PropertyType entity)
        {
            return PropertyTypeMappingExtensions.ToDto(entity);  
        }
    }
}
