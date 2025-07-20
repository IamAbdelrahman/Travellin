using Travellin.Core.Entities;

namespace Travellin.Infrastructure.Data.Seeds
{
    static class LocationSeed
    {
        public static List<Location> Data => new()
        {

            new Location { Id = 1, Name = "Nazlet El-Semman, Giza Governorate", CountryId = 64 },
            new Location { Id = 2, Name = "Milan, Lombardia", CountryId = 106 },
            new Location { Id = 3, Name = "Makkah, Makkah Province", CountryId = 191 },
            new Location { Id = 4, Name = "Yucca Valley, California", CountryId = 231 },
            new Location { Id = 5, Name = "Salvador, Bahia", CountryId = 31 },
            new Location { Id = 6, Name = "Barcelona, Catalunya", CountryId = 205 },
            new Location { Id = 7, Name = "Wadi Rum Village, Aqaba Governorate", CountryId = 110 },
            new Location { Id = 8, Name = "Tanneron, Provence-Alpes-Côte d'Azur", CountryId = 74 },
            new Location { Id = 9, Name = "Windmill in Ponta Delgada, Portugal", CountryId = 174 },
            new Location { Id = 10, Name = "Soul ,South Korea  ", CountryId = 114 },
            new Location { Id = 11, Name = "Cape Town, Western Cape, South Africa", CountryId = 202 },
            new Location { Id = 12, Name = "Maadi , Egypt", CountryId = 64 },
            new Location { Id = 13, Name = "Courtenay, Canada", CountryId = 39 },
            new Location { Id = 14, Name = "Virginia, United States", CountryId = 231 },
            new Location { Id = 15, Name = " Gammarth, Tunisia", CountryId = 223 },
            new Location { Id = 16, Name = "Jodoigne, Belgium", CountryId = 22 },
            new Location { Id = 17, Name = "Buenos Aires, Argentina", CountryId = 11 },
            new Location { Id = 18, Name = "Lekki, Nigeria", CountryId = 157 },
            new Location { Id = 19, Name = "Lâm Thượng, Vietnam", CountryId = 236 },
            new Location { Id = 20, Name = "Dubai, United Arab Emirates", CountryId = 229 },
            new Location { Id = 21, Name = "Imlil, Morocco", CountryId = 146 },
            new Location { Id = 22, Name = " Sasaima,  Colombia", CountryId = 48 },
            new Location { Id = 23, Name = "Kabupaten Gianyar, Indonesia", CountryId = 101 },
            new Location { Id = 24, Name = "Rome, Italy", CountryId = 106 },
            new Location { Id = 25, Name = "Bodrum, Turkey", CountryId = 224 }
        };
    }
}
