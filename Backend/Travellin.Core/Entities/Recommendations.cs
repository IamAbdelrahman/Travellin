using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travellin.Core.Entities
{
    public class Recommendations:BaseEntity<int>
    {
        public string UserId { get; set; }  
        public string Query { get; set; }
        public string PropertyId { get; set; }
        public double Score { get; set; }   
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
