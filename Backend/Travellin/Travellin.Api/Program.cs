using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Travellin.Api.Filters;
using Travellin.Api.Utils;
using Travellin.Core.Interfaces;
using Travellin.Core.Mappings;
using Travellin.Infrastructure;

namespace Travellin.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.ConfigureInfrastructure(builder.Configuration);
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
                          .WithHeaders("Authorization", "Content-Type", "X-Requested-With");
                });

            });

            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            app.UseIpRateLimiting();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "v1"));
            }


            app.UseHttpsRedirection();

            app.UseCors(builder.Configuration["Cors:Policy"]);

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

            app.Run();
        }
    }
}