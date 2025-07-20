using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Infrastructure.Data;

namespace Travellin.Infrastructure.Repositories
{
    class CountryRepository : GenericRepository<Country, int>, ICountryRepository
    {
        public CountryRepository(TravellinDbContext dbContext) : base(dbContext)
        { }
    }
}
