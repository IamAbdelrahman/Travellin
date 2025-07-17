namespace Travellin.Travellin.Core.Entities
{
    public class Conversation : BaseEntity
    {
        public Guid User1Id { get; set; }
        public Guid User2Id { get; set; }

        // Navigation properties
        public User User1 { get; set; }
        public User User2 { get; set; }
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
