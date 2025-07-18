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
        public List<Notification> GetAll()
        {
            return db.Notifications.ToList();
        }

        //GetByID
        public Notification GetByID(int id)
        {
            return db.Notifications.Find(id);
        }

        //Add
        public void Add(Notification entity)
        {
            db.Add(entity);
        }

        //Delete
        public void Delete(int id)
        {
            Notification notification = GetByID(id);
            db.Notifications.Remove(notification);
        }

        //Update
        public void Update(Notification entity)
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
