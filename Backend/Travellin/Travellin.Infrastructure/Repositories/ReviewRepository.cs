using Microsoft.EntityFrameworkCore;
using Travellin.Travellin.Core.Entities;
using Travellin.Travellin.Core.Interfaces;
using Travellin.Travellin.Infrastructure.Data;

namespace Travellin.Travellin.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AirbnbDbContext db;

        public ReviewRepository(AirbnbDbContext db)
        {
            this.db = db;
        }

        //GetALL
        public async Task<List<Review>> GetAllAsync()
        {
            return await db.Reviews.ToListAsync();
        }

        //GetByID
        public async Task<Review> GetByIDAsync(int id)
        {
            return await db.Reviews.FindAsync(id);
        }

        //Add
        public async Task AddAsync(Review entity)
        {
            await db.Reviews.AddAsync(entity);
        }

        //Delete
        public void Delete(int id)
        {
            var review = db.Reviews.Find(id);
            if (review != null)
            {
                db.Reviews.Remove(review);
            }
        }

        //Update
        public void Update(Review entity)
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
