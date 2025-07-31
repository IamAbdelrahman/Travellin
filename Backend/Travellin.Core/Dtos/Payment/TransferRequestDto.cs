using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travellin.Core.Dtos.Payment
{
    public class TransferRequestDto
    {
        public string PaymentIntentId { get; set; }
        public string HostStripeAccountId { get; set; }
        public long AmountInCents { get; set; }
    }
}
