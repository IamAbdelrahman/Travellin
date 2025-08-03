using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

            // Configure Ratelimiting
            builder.Services.ConfigureRateLimiting(builder.Configuration);
            builder.Services.Configure<StripeOptions>(builder.Configuration.GetSection("Stripe"));
            StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretApiKey"];
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
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

                    policy.WithOrigins(allowedOrigins)
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials() // For cookies
                          .WithHeaders("Authorization", "Content-Type", "X-Requested-With", "x-signalr-user-agent", "x-negotiateversion");
                });

            });


            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            });

            // Add custom user provider for SignalR
            builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();

            var app = builder.Build();


            app.UseIpRateLimiting();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "v1"));
            }

            app.UseHttpsRedirection();
            app.UseCors(builder.Configuration["Cors:Policy"] ?? "AllowAll");

            // Initialize FileUploadPathMappingExtensions with the service provider
            using (var scope = app.Services.CreateScope())
            {
                var httpContextAccessor = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
                FileUploadPathMappingExtensions.Init(app.Configuration, httpContextAccessor);
            }
            ;

            app.UseCustomStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapHub<ChatHub>("/hubs/chat");
            app.MapHub<NotificationHub>("/hubs/notification");
            app.Run();
        }
    }
}