using Microsoft.EntityFrameworkCore;
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
        public async Task<List<Message>> GetAllAsync()
        {
            return await db.Messages.ToListAsync();
        }

        //GetByID
        public async Task<Message> GetByIDAsync(int id)
        {
            return await db.Messages.FindAsync(id);
        }

        //Add
        public async Task AddAsync(Message entity)
        {
            await db.Messages.AddAsync(entity);
        }

        //Delete
        public void Delete(int id)
        {
            var message = db.Messages.Find(id);
            if (message != null)
            {
                db.Messages.Remove(message);
            }
        }


        //Update
        public void Update(Message entity)
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
