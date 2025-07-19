using Microsoft.EntityFrameworkCore;
using Travellin.Travellin.Core.Entities;
using Travellin.Travellin.Core.Interfaces;
using Travellin.Travellin.Infrastructure.Data;

namespace Travellin.Travellin.Infrastructure.Repositories
{
    public class NotificationRepository :INotificationRepository
    {
        private readonly AirbnbDbContext db;

        public NotificationRepository(AirbnbDbContext db)
        {
            this.db = db;
        }

        //GetALL
        public async Task<List<Notification>> GetAllAsync()
        {
            return await db.Notifications.ToListAsync();
        }

        //GetByID
        public async Task<Notification> GetByIDAsync(int id)
        {
            return await db.Notifications.FindAsync(id);
        }

        //Add
        public async Task AddAsync(Notification entity)
        {
            await db.Notifications.AddAsync(entity);
        }

        //Delete
        public void Delete(int id)
        {
            var notification = db.Notifications.Find(id);
            if (notification != null)
            {
                db.Notifications.Remove(notification);
            }
        }

        //Update
        public void Update(Notification entity)
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
