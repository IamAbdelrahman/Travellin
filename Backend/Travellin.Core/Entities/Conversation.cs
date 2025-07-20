using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travellin.Core.Entities
{
    public class Conversation: BaseEntity<int>
    {
        public Guid User1Id { get; set; }
        public Guid User2Id { get; set; }

        public AppUser User1 { get; set; }
        public AppUser User2 { get; set; }
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
