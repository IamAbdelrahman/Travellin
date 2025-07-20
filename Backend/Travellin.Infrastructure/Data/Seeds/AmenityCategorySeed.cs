using Travellin.Core.Entities;

namespace Travellin.Infrastructure.Data.Seeds
{
    static class AmenityCategorySeed
    {
        public static List<AmenityCategory> Data => new()
        {
            new AmenityCategory {Id = 1, Name="Essentials"},
            new AmenityCategory {Id = 2, Name="Kitchen and dining"},
            new AmenityCategory {Id = 3, Name="Home safety"},
            new AmenityCategory {Id = 4, Name="Entertainment"},
            new AmenityCategory {Id = 5, Name="Outdoor"},
            new AmenityCategory {Id = 6, Name="Parking and facilities"},
            new AmenityCategory {Id = 7, Name="Heating and cooling"},
            new AmenityCategory {Id = 8, Name="Bedroom and laundry"},
            new AmenityCategory {Id = 9, Name="Bathroom"},
            new AmenityCategory {Id = 10, Name="Family"},
            new AmenityCategory {Id = 11, Name="Internet and office"},
            new AmenityCategory {Id = 12, Name="Location features"},
            new AmenityCategory {Id = 13, Name="Services"},
        };
    }
}
