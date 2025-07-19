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
        public List<Conversation> GetAll()
        {
            return db.Conversations.ToList();
        }

        //GetByID
        public Conversation GetByID(int id)
        {
            return db.Conversations.Find(id);
        }

        //Add
        public void Add(Conversation entity)
        {
            db.Add(entity);
        }

        //Delete
        public void Delete(int id)
        {
            Conversation conversation = GetByID(id);
            db.Conversations.Remove(conversation);
        }

        //Update
        public void Update(Conversation entity)
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
