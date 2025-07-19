using Microsoft.EntityFrameworkCore;
using Travellin.Travellin.Core.Interfaces;
using Travellin.Travellin.Infrastructure.Data;
<<<<<<< HEAD
using Travellin.Travellin.Infrastructure.Shared;
=======
using Travellin.Api.Filters;
using Travellin.Api.Helpers;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
>>>>>>> main

namespace Travellin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AirbnbDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("CFG")));
            builder.Services.AddControllers();

            // Add controllers with global filters
            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<ExceptionFilter>();
            });

            
            builder.Services.AddScoped<AuthorizeHostFilter>();
            builder.Services.AddScoped<ExceptionFilter>();

            // Configure API behavior options for consistent error responses
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            // Add middleware for global error handling (alternative to the filter)
            app.UseMiddleware<ExceptionHandlerMiddleware>(); // You would need to implement this

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}