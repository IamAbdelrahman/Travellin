using Microsoft.EntityFrameworkCore;
using Travellin.Travellin.Core.Entities;
using Travellin.Travellin.Core.Interfaces;
using Travellin.Travellin.Infrastructure.Data;

namespace Travellin.Travellin.Infrastructure.Repositories
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly AirbnbDbContext db;

        public ConversationRepository(AirbnbDbContext db)
        {
            this.db = db;
        }

        //GetALL
        public async Task<List<Conversation>> GetAllAsync()
        {
            return await db.Conversations.ToListAsync();
        }

        //GetByID
        public async Task<Conversation> GetByIDAsync(int id)
        {
            return await db.Conversations.FindAsync(id);
        }

        //Add
        public async Task AddAsync(Conversation entity)
        {
            await db.Conversations.AddAsync(entity);
        }

        //Delete
        public void Delete(int id)
        {
            var conversation = db.Conversations.Find(id);
            if (conversation != null)
            {
                db.Conversations.Remove(conversation);
            }
        }


        //Update
        public void Update(Conversation entity)
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
