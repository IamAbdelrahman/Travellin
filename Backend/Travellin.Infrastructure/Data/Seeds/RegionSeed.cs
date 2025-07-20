using Travellin.Core.Entities;

namespace Travellin.Infrastructure.Data.Seeds
{
    static class RegionSeed
    {
        public static List<Region> Data => new()
        {
            new Region {Id = 1, Name ="North America"},
            new Region {Id = 2, Name ="Europe"},
            new Region {Id = 3, Name ="Asia"},
            new Region {Id = 4, Name ="South America"},
            new Region {Id = 5, Name ="Africa & Oceania"},
        };
    }
}
