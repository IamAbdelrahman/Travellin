using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Travellin.Core.Interfaces;

namespace Travellin.Infrastructure.Services
{
    public class StripeTransferService : IStripeTransferService
    {
        private readonly string _secretKey;
        private readonly ILogger<StripeTransferService> _logger;

        public StripeTransferService(IConfiguration config, ILogger<StripeTransferService> logger)
        {
            _secretKey = config["Stripe:SecretApiKey"];
            StripeConfiguration.ApiKey = _secretKey;
            _logger = logger;
        }

        public async Task<Transfer> TransferToHostAsync(string paymentIntentId, string hostStripeAccountId, long amountInCents)
        {
            try
            {
                var paymentIntentService = new PaymentIntentService();
                var paymentIntent = await paymentIntentService.GetAsync(paymentIntentId, new PaymentIntentGetOptions
                {
                    Expand = new List<string> { "latest_charge" } // بدل charges نجيب latest_charge
                });

                if (paymentIntent == null)
                    throw new InvalidOperationException("PaymentIntent not found.");
                if (paymentIntent.Status != "succeeded")
                    throw new InvalidOperationException($"PaymentIntent status is {paymentIntent.Status}; it must be 'succeeded' to transfer.");

                // نحاول نجيب chargeId من latest_charge مباشرة
                var chargeId = paymentIntent.LatestChargeId;

                // fallback في حالة مفيش latest_charge
                if (string.IsNullOrEmpty(chargeId))
                {
                    var chargeService = new ChargeService();
                    var charges = await chargeService.ListAsync(new ChargeListOptions
                    {
                        PaymentIntent = paymentIntentId,
                        Limit = 1
                    });

                    chargeId = charges.FirstOrDefault()?.Id;
                }

                if (string.IsNullOrEmpty(chargeId))
                    throw new InvalidOperationException("No charge associated with this PaymentIntent.");

                // عمل التحويل
                var transferService = new TransferService();
                var transferOptions = new TransferCreateOptions
                {
                    Amount = amountInCents,
                    Currency = "usd",
                    Destination = hostStripeAccountId,
                    SourceTransaction = chargeId
                };

                var transfer = await transferService.CreateAsync(transferOptions);
                _logger.LogInformation("Transfer {TransferId} created successfully for host {HostId} with amount {Amount} cents.",
                    transfer.Id, hostStripeAccountId, amountInCents);

                return transfer;
            }
            catch (StripeException ex)
            {
                _logger.LogError(ex, "Stripe transfer failed: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during transfer: {Message}", ex.Message);
                throw;
            }
        }
    }
}
