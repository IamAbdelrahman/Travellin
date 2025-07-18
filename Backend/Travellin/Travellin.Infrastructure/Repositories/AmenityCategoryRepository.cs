using Travellin.Travellin.Core.Entities;
using Travellin.Travellin.Core.Interfaces;
using Travellin.Travellin.Infrastructure.Data;

namespace Travellin.Travellin.Infrastructure.Repositories
{
    public class AmenityCategoryRepository : IAmenityCategoryRepository
    {
        private readonly AirbnbDbContext db;

        public AmenityCategoryRepository(AirbnbDbContext db)
        {
            this.db = db;
        }

        //GetALL
        public List<AmenityCategory> GetAll()
        {
            return db.AmenityCategories.ToList();
        }

        //GetByID
        public AmenityCategory GetByID(int id)
        {
            return db.AmenityCategories.Find(id);
        }

        //Add
        public void Add(AmenityCategory entity)
        {
            db.Add(entity);
        }

        //Delete
        public void Delete(int id)
        {
            AmenityCategory category = GetByID(id);
            db.AmenityCategories.Remove(category);
        }

        //Update
        public void Update(AmenityCategory entity)
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
