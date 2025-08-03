using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Travellin.Infrastructure.Hubs
{
    [Authorize]
    public class NotificationHub : Hub
    {
        private readonly ILogger<NotificationHub> _logger;

        public NotificationHub(ILogger<NotificationHub> logger)
        {
            _logger = logger;
        }

        private string GetCurrentUserId()
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("User not authenticated");
            }
            return userId;
        }

        public async Task TestConnection()
        {
            try
            {
                var userId = GetCurrentUserId();
                _logger.LogInformation("NotificationHub TestConnection called by user {UserId}", userId);
                
                await Clients.Caller.SendAsync("TestResponse", $"Notification connection test successful for user {userId}");
                _logger.LogInformation("Notification test response sent to user {UserId}", userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in NotificationHub TestConnection");
                await Clients.Caller.SendAsync("ReceiveError", "Notification test connection failed.");
            }
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                var userId = GetCurrentUserId();
                _logger.LogInformation("=== USER CONNECTING TO NOTIFICATION HUB ===");
                _logger.LogInformation("User ID: {UserId}", userId);
                _logger.LogInformation("Connection ID: {ConnectionId}", Context.ConnectionId);
                
                await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userId}");
                _logger.LogInformation("User {UserId} added to notification group user_{UserId}", userId, userId);
                
                _logger.LogInformation("User {UserId} connected to notification hub successfully", userId);
                await base.OnConnectedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in NotificationHub OnConnectedAsync");
                throw;
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            try
            {
                var userId = GetCurrentUserId();
                _logger.LogInformation("=== USER DISCONNECTING FROM NOTIFICATION HUB ===");
                _logger.LogInformation("User ID: {UserId}", userId);
                _logger.LogInformation("Connection ID: {ConnectionId}", Context.ConnectionId);
                
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"user_{userId}");
                _logger.LogInformation("User {UserId} removed from notification group user_{UserId}", userId, userId);
                
                _logger.LogInformation("User {UserId} disconnected from notification hub", userId);
                await base.OnDisconnectedAsync(exception);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in NotificationHub OnDisconnectedAsync");
                await base.OnDisconnectedAsync(exception);
            }
        }
    }
} 