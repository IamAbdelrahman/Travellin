using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Stripe;
using System.Security.Claims;
using Travellin.Api.Filters;
using Travellin.Api.Hubs;
using Travellin.Infrastructure.Hubs;
using Travellin.Api.Utils;
using Travellin.Core.Interfaces;
using Travellin.Core.Mappings;
using Travellin.Core.Services;
using Travellin.Infrastructure;
using Travellin.Infrastructure.Repositories;
using Travellin.Infrastructure.Services;

namespace Travellin.Api
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            var userId = connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userId;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.ConfigureInfrastructure(builder.Configuration);

            builder.Services.PostConfigure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Events ??= new JwtBearerEvents();
                var originalOnMessageReceived = options.Events.OnMessageReceived;

                options.Events.OnMessageReceived = async context =>
                {
                    var accessToken = context.Request.Query["access_token"];
                    var path = context.HttpContext.Request.Path;

                    if (!string.IsNullOrEmpty(accessToken) &&
                        (path.StartsWithSegments("/hubs/chat") || path.StartsWithSegments("/hubs/notification")))
                    {
                        context.Token = accessToken;
                    }
                    else if (originalOnMessageReceived != null)
                    {
                        await originalOnMessageReceived(context);
                    }
                };
            });

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<ErrorHandlingFilter>();
            });

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            // Rate limiting
            builder.Services.ConfigureRateLimiting(builder.Configuration);

            // Stripe
            builder.Services.Configure<StripeOptions>(builder.Configuration.GetSection("Stripe"));
            StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretApiKey"];

            // Swagger / OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            });

            builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();

            var app = builder.Build();

            // Middlewares
            app.UseIpRateLimiting();

            // Swagger UI setup
            if (app.Environment.IsDevelopment() || true) // force enable swagger
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Travellin API v1");
                    options.RoutePrefix = "swagger"; // Swagger available at /swagger
                });
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");

            using (var scope = app.Services.CreateScope())
            {
                var httpContextAccessor = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
                FileUploadPathMappingExtensions.Init(app.Configuration, httpContextAccessor);
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCustomStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapHub<ChatHub>("/hubs/chat");
            app.MapHub<NotificationHub>("/hubs/notification");

            // Angular SPA fallback
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}
