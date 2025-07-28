using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travellin.Core.Dtos.Accounts
{
    public class GoogleUserDto
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhotoUrl { get; set; }
        public string ProviderId { get; set; }
    }
}
