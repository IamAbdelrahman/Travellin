using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Travellin.Core.Dtos.Notifications;
using Travellin.Core.Interfaces;
using Travellin.Travellin.Core.Enums;

namespace Travellin.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly ILogger<NotificationsController> _logger;

        public NotificationsController(INotificationService notificationService, ILogger<NotificationsController> logger)
        {
            _notificationService = notificationService;
            _logger = logger;
        }

        private string GetCurrentUserId() =>
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

        [HttpGet]
        public async Task<ActionResult<List<NotificationDto>>> GetUserNotifications(
            [FromQuery] bool includeRead = false,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            try
            {
                var userId = GetCurrentUserId();
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var notifications = await _notificationService.GetUserNotificationsAsync(userId, includeRead, page, pageSize);
                return Ok(notifications);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user notifications");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("unread-count")]
        public async Task<ActionResult<int>> GetUnreadCount()
        {
            try
            {
                var userId = GetCurrentUserId();
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var count = await _notificationService.GetUnreadCountAsync(userId);
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting unread count");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("{id}/mark-as-read")]
        public async Task<ActionResult> MarkAsRead(int id)
        {
            try
            {
                await _notificationService.MarkAsReadAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking notification as read");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("mark-all-as-read")]
        public async Task<ActionResult> MarkAllAsRead()
        {
            try
            {
                var userId = GetCurrentUserId();
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                await _notificationService.MarkAllAsReadAsync(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking all notifications as read");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNotification(int id)
        {
            try
            {
                await _notificationService.DeleteNotificationAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting notification");
                return StatusCode(500, "Internal server error");
            }
        }

        // Booking Notifications
        [HttpPost("booking-request")]
        [Authorize(Roles = "Host")]
        public async Task<ActionResult> NotifyBookingRequest([FromBody] BookingRequestNotificationDto bookingRequest)
        {
            try
            {
                var hostId = GetCurrentUserId();
                await _notificationService.NotifyBookingRequestAsync(hostId, bookingRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending booking request notification");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("booking-response")]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult> NotifyBookingResponse([FromBody] BookingResponseNotificationDto bookingResponse)
        {
            try
            {
                var guestId = GetCurrentUserId();
                await _notificationService.NotifyBookingResponseAsync(guestId, bookingResponse);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending booking response notification");
                return StatusCode(500, "Internal server error");
            }
        }

        // Payment Notifications
        [HttpPost("payment-success")]
        public async Task<ActionResult> NotifyPaymentSuccess([FromBody] PaymentNotificationDto payment)
        {
            try
            {
                var userId = GetCurrentUserId();
                await _notificationService.NotifyPaymentSuccessAsync(userId, payment);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending payment success notification");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("payment-failed")]
        public async Task<ActionResult> NotifyPaymentFailed([FromBody] PaymentNotificationDto payment)
        {
            try
            {
                var userId = GetCurrentUserId();
                await _notificationService.NotifyPaymentFailedAsync(userId, payment);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending payment failed notification");
                return StatusCode(500, "Internal server error");
            }
        }

        // Review Notifications
        [HttpPost("review-received")]
        [Authorize(Roles = "Host")]
        public async Task<ActionResult> NotifyReviewReceived([FromBody] ReviewNotificationDto review)
        {
            try
            {
                var hostId = GetCurrentUserId();
                await _notificationService.NotifyReviewReceivedAsync(hostId, review);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending review received notification");
                return StatusCode(500, "Internal server error");
            }
        }

        // Host Notifications
        [HttpPost("host-upgrade")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> NotifyHostUpgrade([FromBody] HostUpgradeNotificationDto upgradeRequest)
        {
            try
            {
                var adminId = GetCurrentUserId();
                await _notificationService.NotifyHostUpgradeRequestAsync(adminId, upgradeRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending host upgrade notification");
                return StatusCode(500, "Internal server error");
            }
        }

        // Guest Notifications
        [HttpPost("guest-arrival")]
        [Authorize(Roles = "Host")]
        public async Task<ActionResult> NotifyGuestArrival([FromBody] GuestArrivalNotificationDto arrival)
        {
            try
            {
                var hostId = GetCurrentUserId();
                await _notificationService.NotifyGuestArrivalAsync(hostId, arrival);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending guest arrival notification");
                return StatusCode(500, "Internal server error");
            }
        }

        // System Notifications
        [HttpPost("system")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> NotifySystem([FromBody] SystemNotificationDto systemNotification)
        {
            try
            {
                var userId = GetCurrentUserId();
                await _notificationService.NotifyPromotionAsync(userId, systemNotification);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending system notification");
                return StatusCode(500, "Internal server error");
            }
        }

        // Bulk Notifications (Admin only)
        [HttpPost("bulk/hosts")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> NotifyAllHosts([FromBody] BulkNotificationRequest request)
        {
            try
            {
                await _notificationService.NotifyAllHostsAsync(request.Type, request.Content, request.Metadata);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending bulk notification to hosts");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("bulk/guests")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> NotifyAllGuests([FromBody] BulkNotificationRequest request)
        {
            try
            {
                await _notificationService.NotifyAllGuestsAsync(request.Type, request.Content, request.Metadata);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending bulk notification to guests");
                return StatusCode(500, "Internal server error");
            }
        }
    }

    public class BulkNotificationRequest
    {
        public NotificationType Type { get; set; }
        public string Content { get; set; }
        public Dictionary<string, object>? Metadata { get; set; }
    }
}
