using Travellin.Travellin.Core.Entities;
using Travellin.Travellin.Core.Interfaces;
using Travellin.Travellin.Infrastructure.Data;

namespace Travellin.Travellin.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AirbnbDbContext db;

        public MessageRepository(AirbnbDbContext db)
        {
            this.db = db;
        }

        //GetALL
        public List<Message> GetAll()
        {
            return db.Messages.ToList();
        }

        //GetByID
        public Message GetByID(int id)
        {
            return db.Messages.Find(id);
        }

        //Add
        public void Add(Message entity)
        {
            db.Add(entity);
        }

        //Delete
        public void Delete(int id)
        {
            Message message = GetByID(id);
            db.Messages.Remove(message);
        }

        //Update
        public void Update(Message entity)
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
