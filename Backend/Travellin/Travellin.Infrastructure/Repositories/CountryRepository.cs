using Microsoft.EntityFrameworkCore;
using Travellin.Travellin.Core.Entities;
using Travellin.Travellin.Core.Interfaces;
using Travellin.Travellin.Infrastructure.Data;

namespace Travellin.Travellin.Infrastructure.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly AirbnbDbContext db;

        public CountryRepository(AirbnbDbContext db)
        {
            this.db = db;
        }

        //GetALL
        public async Task<List<Country>> GetAllAsync()
        {
            return await db.Countries.ToListAsync();
        }

        //GetByID
        public async Task<Country> GetByIDAsync(int id)
        {
            return await db.Countries.FindAsync(id);
        }

        //Add
        public async Task AddAsync(Country entity)
        {
            await db.Countries.AddAsync(entity);
        }

        //Delete
        public void Delete(int id)
        {
            var country = db.Countries.Find(id);
            if (country != null)
            {
                db.Countries.Remove(country);
            }
        }


        //Update
        public void Update(Country entity)
        {
            db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        //Save
        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
