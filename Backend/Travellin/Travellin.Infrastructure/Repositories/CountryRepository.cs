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
        public List<Country> GetAll()
        {
            return db.Countries.ToList();
        }

        //GetByID
        public Country GetByID(int id)
        {
            return db.Countries.Find(id);
        }

        //Add
        public void Add(Country entity)
        {
            db.Add(entity);
        }

        //Delete
        public void Delete(int id)
        {
            Country country = GetByID(id);
            db.Countries.Remove(country);
        }

        //Update
        public void Update(Country entity)
        {
            db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        //Save
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
