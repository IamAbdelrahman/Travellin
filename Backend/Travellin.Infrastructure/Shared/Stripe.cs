using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travellin.Infrastructure.Shared
{

    public class StripeOptions
    {
        public string SecretApiKey { get; set; }
        public string PublishableKey { get; set; }
    }

}
