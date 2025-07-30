using Microsoft.AspNetCore.Mvc;
using Stripe;
using Travellin.Api.Controllers;
using Travellin.Core.Dtos.Payment;
using Travellin.Core.Interfaces;

namespace TRavellin.Api.Controllers
{
    public class PaymentsController : BaseController
    {
        private readonly IServiceFactory _serviceFactory;
        private readonly IStripeTransferService _transferService;
        public PaymentsController(IUnitOfWork unitOfWork, IServiceFactory serviceFactory, IStripeTransferService transferService)
            : base(unitOfWork)
        {
            _serviceFactory = serviceFactory;
            _transferService=transferService;
        }

        [HttpPost("create-checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] CheckoutOptions options)
        {
            var result = await _serviceFactory.CheckoutManagementService.CreateCheckoutSessionAsync(options);
            return Ok(result);
        }

        [HttpPost("stripe/webhook")]
        public async Task<IActionResult> StripeWebHook()
        {
            try
            {
                var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
                var signature = Request.Headers["Stripe-Signature"];
                await _serviceFactory.CheckoutManagementService.HandlePaymentWebhookAsync(json, signature);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log exception
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("transfer-to-host")]
        public async Task<IActionResult> TransferToHost([FromBody] TransferRequestDto dto)
        {
            try
            {
                var transfer = await _transferService.TransferToHostAsync(dto.PaymentIntentId, dto.HostStripeAccountId, dto.AmountInCents);
                return Ok(new { transfer.Id});
            }
            catch (StripeException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}