using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System.Text;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Infrastructure.Data;
using Travellin.Infrastructure.Repositories;
using Travellin.Infrastructure.Services;
using Travellin.Infrastructure.Shared;

namespace Travellin.Infrastructure
{
    // POCO for binding Stripe settings from configuration
    public class StripeOptions
    {
        public string SecretApiKey { get; set; }
    }

    public static class InfrastructureConfigurationExtension
    {
        public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Logging service
            services.AddLogging();
            services.AddDbContext<TravellinDbContext>(options
                    => options.UseSqlServer(configuration.GetConnectionString("CFG")));
            services.AddEndpointsApiExplorer();
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 12;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = false;
            }).AddEntityFrameworkStores<TravellinDbContext>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false, // TODO: Turnned in prod environment
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audiance"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SigningKey"]))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = ctx =>
                    {
                        ctx.Request.Cookies.TryGetValue("access_token", out var accessToken);

                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            ctx.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            // OpenAI configuration
            services.AddHttpClient();

                // Configure StripeOptions binding and register a singleton StripeClient
                services.Configure<StripeOptions>(configuration.GetSection("Stripe"));
                services.AddSingleton<StripeClient>(sp =>
                {
                    var stripeOptions = sp.GetRequiredService<IOptions<StripeOptions>>().Value;
                    return new StripeClient(stripeOptions.SecretApiKey);
                });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IIdentityFactory, IdentityFactory>();
            services.AddScoped<IFileStorageService, FileStorageService>();
            services.AddScoped<IServiceFactory, ServiceFactory>();
            //Messaging Services
            services.AddScoped<IConversationService, ConversationService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IConversationRepository, ConversationRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            return services;
        }
    }
}