using Travellin.Core.Entities;

namespace Travellin.Infrastructure.Data.Seeds
{
    static class PropertySpaceTypeSeed
    {
        public static List<PropertySpaceType> Data => new()
        {
            new PropertySpaceType { Id = 1, Name = "Bedroom" },
            new PropertySpaceType { Id = 2, Name = "Bathroom" },
            new PropertySpaceType { Id = 3, Name = "Kitchen" },
            new PropertySpaceType { Id = 4, Name = "Living Room" },
            new PropertySpaceType { Id = 5, Name = "Dining Area" },
            new PropertySpaceType { Id = 6, Name = "Workspace" },
            new PropertySpaceType { Id = 7, Name = "Laundry Area" },
            new PropertySpaceType { Id = 8, Name = "Private Entrance" },
            new PropertySpaceType { Id = 9, Name = "Balcony" },
            new PropertySpaceType { Id = 10, Name = "Patio" },
            new PropertySpaceType { Id = 11, Name = "Backyard" },
            new PropertySpaceType { Id = 12, Name = "Fire Pit" },
            new PropertySpaceType { Id = 13, Name = "Baby Room" },
            new PropertySpaceType { Id = 14, Name = "Children’s Play Area" },
            new PropertySpaceType { Id = 15, Name = "Closet" },
            new PropertySpaceType { Id = 16, Name = "Storage Space" }
        };
    }
}
