using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Stripe;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace Travellin.Core.Interfaces
{

    public interface IStripeTransferService
    {
        Task<Transfer> TransferToHostAsync(string paymentIntentId, string hostStripeAccountId, long amountInCents);
    }

}
