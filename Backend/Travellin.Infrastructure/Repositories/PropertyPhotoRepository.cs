using Microsoft.EntityFrameworkCore;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Infrastructure.Data;

namespace Travellin.Infrastructure.Repositories
{
    class PropertyPhotoRepository : BaseRepository, IPropertyPhotoRepository
    {
        public PropertyPhotoRepository(TravellinDbContext dbContext) : base(dbContext)
        { }

        public void Create(PropertyPhoto entity)
        {
            _dbContext.Add(entity);
        }

        public async Task<IReadOnlyCollection<PropertyPhoto>> GetAllByPropertyIdASync(string propertyId)
        {
            var query = _dbContext.PropertyPhotos
                .Include(x => x.FileUpload)
                .OrderBy(x => x.TouchedAt)
                .Where(x => x.PropertyId == propertyId)
                .AsQueryable();

            var items = await query.ToListAsync();
            return items;
        }

        public void Update(PropertyPhoto entity)
        {
            _dbContext.Update(entity);
        }
    }
}
