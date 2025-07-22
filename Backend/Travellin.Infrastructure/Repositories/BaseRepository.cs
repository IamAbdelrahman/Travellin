using Travellin.Core.Interfaces;
using Travellin.Infrastructure.Data;

namespace Travellin.Infrastructure.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        protected readonly TravellinDbContext _dbContext;

        public BaseRepository(TravellinDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
