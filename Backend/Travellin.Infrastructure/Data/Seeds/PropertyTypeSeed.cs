using Travellin.Core.Entities;

namespace Travellin.Infrastructure.Data.Seeds
{
    static class PropertyTypeSeed
    {
        public static List<PropertyType> Data => new()
        {
            new PropertyType { Id = 1, Name = "House/Apartment", Icon = "house" },
            new PropertyType { Id = 2, Name = "Room", Icon = "bed-single" },
            new PropertyType { Id = 3, Name = "Hotel", Icon = "hotel" },
            new PropertyType { Id = 4, Name = "Unique & Themed Stays", Icon = "sun-moon" }
        };
    }
}
