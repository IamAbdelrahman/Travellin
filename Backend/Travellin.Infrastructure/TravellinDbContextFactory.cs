using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travellin.Infrastructure.Data;

namespace Travellin.Infrastructure
{
    public class TravellinDbContextFactory : IDesignTimeDbContextFactory<TravellinDbContext>
    {
        public TravellinDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json") // Ensure the path is correct
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<TravellinDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("CFG"));

            return new TravellinDbContext(optionsBuilder.Options);
        }
    }
}
