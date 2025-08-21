using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OpenAI.Chat;
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
using Microsoft.EntityFrameworkCore;

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

            // ===== Add services =====
            builder.Services.ConfigureInfrastructure(builder.Configuration);

            // 🔹 هنا مش مستخدم InMemory
            // إنت بقى تزبط ال ConnectionString في appsettings.json
            //builder.Services.AddDbContext<AppDbContext>(options =>
            //    options.UseInMemoryDatabase("TravellinDb"));

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

            // Swagger/OpenAPI
            builder.Services.AddOpenApi();

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

                options.AddPolicy("AllowTrusted", policy =>
                {
                    var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
                    policy.WithOrigins(allowedOrigins ?? Array.Empty<string>())
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
            });

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            });

            // Custom user provider
            builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();

            var app = builder.Build();

            // ===== Middleware pipeline =====
            app.UseIpRateLimiting();

            // Swagger
            app.MapOpenApi();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/v1.json", "Travellin API v1");
                options.RoutePrefix = "swagger";
            });

            // NOTE: خلي HTTPS redirect مقفول عشان Render
            // app.UseHttpsRedirection();

            app.UseCors(builder.Configuration["Cors:Policy"] ?? "AllowAll");

            // Static files
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCustomStaticFiles();

            app.UseRouting();

            // Auth
            app.UseAuthentication();
            app.UseAuthorization();

            // Controllers & Hubs
            app.MapControllers();
            app.MapHub<ChatHub>("/hubs/chat");
            app.MapHub<NotificationHub>("/hubs/notification");

            // Angular fallback
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}
