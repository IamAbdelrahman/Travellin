using Travellin.Travellin.Core.Entities;
using Travellin.Travellin.Infrastructure.Data;

namespace Travellin.Travellin.Infrastructure.Repositories
{
    public class FavouriteRepository
    {
        private readonly AirbnbDbContext db;

        public FavouriteRepository(AirbnbDbContext db)
        {
            this.db = db;
        }

        //GetALL
        public List<Favourite> GetAll()
        {
            return db.Favourites.ToList();
        }

        //GetByID
        public Favourite GetByID(int id)
        {
            return db.Favourites.Find(id);
        }

        //Add
        public void Add(Favourite entity)
        {
            db.Add(entity);
        }

        //Delete
        public void Delete(int id)
        {
            Favourite favourite = GetByID(id);
            db.Favourites.Remove(favourite);
        }

        //Update
        public void Update(Favourite entity)
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
