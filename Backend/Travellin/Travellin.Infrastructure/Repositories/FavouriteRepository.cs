using Microsoft.EntityFrameworkCore;
using Travellin.Travellin.Core.Entities;
using Travellin.Travellin.Core.Interfaces;
using Travellin.Travellin.Infrastructure.Data;

namespace Travellin.Travellin.Infrastructure.Repositories
{
    public class FavouriteRepository :IFavouriteRepository
    {
        private readonly AirbnbDbContext db;

        public FavouriteRepository(AirbnbDbContext db)
        {
            this.db = db;
        }

        //GetALL
        public async Task<List<Favourite>> GetAllAsync()
        {
            return await db.Favourites.ToListAsync();
        }

        //GetByID
        public async Task<Favourite> GetByIDAsync(int id)
        {
            return await db.Favourites.FindAsync(id);
        }

        //Add
        public async Task AddAsync(Favourite entity)
        {
            await db.Favourites.AddAsync(entity);
        }

        //Delete
        public void Delete(int id)
        {
            var favourite = db.Favourites.Find(id);
            if (favourite != null)
            {
                db.Favourites.Remove(favourite);
            }
        }


        //Update
        public void Update(Favourite entity)
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
