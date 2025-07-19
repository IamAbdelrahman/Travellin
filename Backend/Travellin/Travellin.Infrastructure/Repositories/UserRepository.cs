using Microsoft.EntityFrameworkCore;
using Travellin.Travellin.Core.Entities;
using Travellin.Travellin.Core.Interfaces;
using Travellin.Travellin.Infrastructure.Data;

namespace Travellin.Travellin.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AirbnbDbContext db;

        public UserRepository(AirbnbDbContext db)
        {
            this.db = db;
        }

        //GetALL
        public async Task<List<User>> GetAllAsync()
        {
            return await db.Users.ToListAsync();
        }

        //GetByID
        public async Task<User> GetByIDAsync(int id)
        {
            return await db.Users.FindAsync(id);
        }

        //Add
        public async Task AddAsync(User entity)
        {
            await db.Users.AddAsync(entity);
        }

        //Delete
        public void Delete(int id)
        {
            var user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
            }
        }

        //Update
        public void Update(User entity)
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
