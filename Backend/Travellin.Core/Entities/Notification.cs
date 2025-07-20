using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travellin.Travellin.Core.Enums;

namespace Travellin.Core.Entities
{
    public class Notification: BaseEntity<int>
    {
        public Guid UserId { get; set; }

        public string Name { get; set; } = NotificationType.NewMessage.ToString(); // e.g., "BookingConfirmation", "NewMessage"

        public string Content { get; set; }

        public bool IsRead { get; set; }

        public AppUser User { get; set; }
    }
}
