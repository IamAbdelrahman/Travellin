using Microsoft.AspNetCore.Mvc;
using Travellin.Api.Controllers;
using Travellin.Core.Interfaces;

namespace TRavellin.Api.Controllers
{
    public class PaymentsController : BaseController
    {
        IServiceFactory _serviceFactory;
        public PaymentsController(IUnitOfWork unitOfWork, IServiceFactory serviceFactory) : base(unitOfWork)
        {
            _serviceFactory = serviceFactory;
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
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var signature = Request.Headers["Stripe-Signature"];

            await _serviceFactory.CheckoutManagementService.HandlePaymentWebhookAsync(json, signature);

            return Ok();
        }
    }
}