using Microsoft.EntityFrameworkCore;
using Travellin.Travellin.Core.Entities;
using Travellin.Travellin.Core.Interfaces;
using Travellin.Travellin.Infrastructure.Data;

namespace Travellin.Travellin.Infrastructure.Repositories
{
    public class UserUsedPromotionRepository : IUserUsedPromotionRepository
    {
        private readonly AirbnbDbContext db;

        public UserUsedPromotionRepository(AirbnbDbContext db)
        {
            this.db = db;
        }

        //GetALL
        public async Task<List<UserUsedPromotion>> GetAllAsync()
        {
            return await db.UserUsedPromotions.ToListAsync();
        }

        //GetByID
        public async Task<UserUsedPromotion> GetByIDAsync(int id)
        {
            return await db.UserUsedPromotions.FindAsync(id);
        }

        //Add
        public async Task AddAsync(UserUsedPromotion entity)
        {
            await db.UserUsedPromotions.AddAsync(entity);
        }

        //Delete
        public void Delete(int id)
        {
            var userUsedPromotion = db.UserUsedPromotions.Find(id);
            if (userUsedPromotion != null)
            {
                db.UserUsedPromotions.Remove(userUsedPromotion);
            }
        }

        //Update
        public void Update(UserUsedPromotion entity)
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
