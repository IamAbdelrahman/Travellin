using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Travellin.Core.Shared;
using Travellin.Travellin.Core.Enums;
using Stripe;
using Stripe.Checkout;

namespace Travellin.Infrastructure.Services
{
    public class StripeCheckoutService : ICheckoutManagementService
    {
        private readonly StripeClient _stripeClient;
        private IUnitOfWork _unitOfWork;
        private ILogger<StripeCheckoutService> _logger;
        private readonly string _stripeWebHookSecretKey;
        private readonly string _successUrl;
        private readonly string _cancelurl;
        public StripeCheckoutService(StripeClient stripeClient, IUnitOfWork unitOfWork, ILogger<StripeCheckoutService> logger, IConfiguration config)
        {
            _stripeClient = stripeClient;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _stripeWebHookSecretKey = config["Stripe:WebHookSecretKey"];
            _successUrl = config["Stripe:SuccessUrl"];
            _cancelurl = config["Stripe:CancelUrl"];
        }

        //public async Task<CreateCheckoutResult> CreateCheckoutSessionAsync(CheckoutOptions options)
        //{
        //    var sessionService = new SessionService(_stripeClient);

        //    var sessionOptions = new SessionCreateOptions
        //    {
        //        CustomerEmail = options.Guest.Email,
        //        PaymentMethodTypes = new List<string> { "card" },
        //        LineItems = new List<SessionLineItemOptions>
        //    {
        //        new()
        //        {
        //            PriceData = new SessionLineItemPriceDataOptions
        //            {
        //                UnitAmount = (long)(options.Pricing.TotalAmount * 100),
        //                Currency = "usd",
        //                ProductData = new SessionLineItemPriceDataProductDataOptions
        //                {
        //                    Name = options.Property.Title,
        //                    Description = $"Booking from {options.BookingPeriod.CheckInDate:d} to {options.BookingPeriod.CheckOutDate:d}",
        //                    Images = !string.IsNullOrEmpty(options.Property.MainPhotoUrl)
        //                        ? new List<string> { options.Property.MainPhotoUrl }
        //                        : null
        //                }
        //            },
        //            Quantity = 1
        //        }
        //    },
        //        Mode = "payment",
        //        SuccessUrl = _successUrl,
        //        CancelUrl = _cancelurl,
        //        ClientReferenceId = options.BookingId,
        //        Metadata = options.Metadata
        //    };

        //    var session = await sessionService.CreateAsync(sessionOptions);

        //    var newPayment = new Payment
        //    {
        //        BookingId = options.BookingId,
        //        StripeSessionId = session.Id,
        //        StripePaymentIntentId = session.PaymentIntentId,
        //        Amount = session.AmountTotal.Value / 100m, // Convert from cents to dollars
        //        Currency = session.Currency,
        //        Status = PaymentStatus.Pending,
        //        CreatedAt = DateTime.UtcNow,
        //        UpdatedAt = DateTime.UtcNow
        //    };

        //    _unitOfWork.PaymentRepository.Create(newPayment);
        //    await _unitOfWork.SaveChangesAsync();

        //    return new CreateCheckoutResult
        //    {
        //        SessionId = session.Id,
        //        SessionUrl = session.Url,
        //        ExpiresAt = session.ExpiresAt
        //    };
        //}

        public async Task<CreateCheckoutResult> CreateCheckoutSessionAsync(CheckoutOptions options)
        {
            var sessionService = new SessionService(_stripeClient);

            var sessionOptions = new SessionCreateOptions
            {
                CustomerEmail = options.Guest.Email,
                PaymentMethodTypes = new List<string> { "card"
            },
                LineItems = new List<SessionLineItemOptions> {new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)(options.Pricing.TotalAmount * 100), // تحويل المبلغ إلى سنتات
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = options.Property.Title,
                        Description = $"Booking from {options.BookingPeriod.CheckInDate:d} to {options.BookingPeriod.CheckOutDate:d}",
                        Images = !string.IsNullOrEmpty(options.Property.MainPhotoUrl)
                            ? new List<string> { options.Property.MainPhotoUrl }
                            : null
                    }
                },
                Quantity = 1
            }
        },
                Mode = "payment",
                SuccessUrl = _successUrl,
                CancelUrl = _cancelurl,
                ClientReferenceId = options.BookingId,
                Metadata = options.Metadata,
            };

            var session = await sessionService.CreateAsync(sessionOptions);

            var newPayment = new Payment
            {
                BookingId = options.BookingId,
                StripeSessionId = session.Id,
                StripePaymentIntentId = session.PaymentIntentId,
                Amount = session.AmountTotal.Value / 100m, // تحويل من سنت إلى دولار
                Currency = session.Currency,
                Status = PaymentStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _unitOfWork.PaymentRepository.Create(newPayment);
            await _unitOfWork.SaveChangesAsync();

            return new CreateCheckoutResult
            {
                SessionId = session.Id,
                SessionUrl = session.Url,
                ExpiresAt = session.ExpiresAt
            };
        }
        public async Task HandlePaymentWebhookAsync(string json, string signature)
        {
            var stripeEvent = EventUtility.ConstructEvent(json, signature, _stripeWebHookSecretKey);

            try
            {

                switch (stripeEvent.Type)
                {
                    case EventTypes.CheckoutSessionCompleted:
                        await HandleCheckoutSessionCompleted(stripeEvent);
                        break;

                    case EventTypes.CheckoutSessionExpired:
                        await HandleCheckoutSessionExpired(stripeEvent);
                        break;

                    case EventTypes.CheckoutSessionAsyncPaymentFailed:
                        await HandleCheckoutSessionFailed(stripeEvent);
                        break;

                    default:
                        _logger.LogInformation("Unhandled Stripe event type: {EventType}", stripeEvent.Type);
                        break;
                }
            }
            catch (StripeException ex)
            {
                _logger.LogError(ex, "Stripe webhook processing error");
                throw;
            }
        }

        private async Task HandleCheckoutSessionCompleted(Stripe.Event stripeEvent)
        {
            var session = stripeEvent.Data.Object as Session;
            var sessionId = session.Id;
            var bookingId = session.ClientReferenceId;
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(bookingId);
            var payment = await _unitOfWork.PaymentRepository.GetPaymentBySessionIdAsync(sessionId);

            if (payment is null)
            {
                throw new NotFoundException($"Payment with session id[{sessionId}] is not found");
            }

            if (booking is null)
            {
                throw new NotFoundException($"Booking with id [{bookingId}] is not found.");
            }


            payment.Status = PaymentStatus.Successed;
            payment.StripePaymentIntentId = session.PaymentIntentId;
            _unitOfWork.PaymentRepository.Update(payment);

            booking.Status = BookingStatus.Confirmed;
            _unitOfWork.BookingRepository.Update(booking);

            await _unitOfWork.SaveChangesAsync();
        }

        private async Task HandleCheckoutSessionExpired(Stripe.Event stripeEvent)
        {
            var session = stripeEvent.Data.Object as Session;
            var sessionId = session.Id;
            var payment = await _unitOfWork.PaymentRepository.GetPaymentBySessionIdAsync(sessionId);

            if (payment is null)
            {
                throw new NotFoundException($"Payment with session id[{sessionId}] is not found");
            }

            payment.Status = PaymentStatus.Expired;
            _unitOfWork.PaymentRepository.Update(payment);
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task HandleCheckoutSessionFailed(Stripe.Event stripeEvent)
        {
            var session = stripeEvent.Data.Object as Session;
            var sessionId = session.Id;
            var payment = await _unitOfWork.PaymentRepository.GetPaymentBySessionIdAsync(sessionId);

            if (payment is null)
            {
                throw new NotFoundException($"Payment with session id[{sessionId}] is not found");
            }

            payment.Status = PaymentStatus.Failed;
            payment.StripePaymentIntentId = session.PaymentIntentId;
            _unitOfWork.PaymentRepository.Update(payment);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
