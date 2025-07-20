using Travellin.Core.Entities;


namespace Travellin.Infrastructure.Data.Seeds
{
    static class UserProfileSeed
    {
        public static List<UserProfile> Data => new()
        {
            new UserProfile
            {
                UserId = "2dacdb51-fee9-4479-904c-cafe7dca22a6",
                FirstName = "Marcus",
                LastName = "Dou",
                PhotoId ="c3d1f440-7e0e-4f38-8b5d-34ea8d12e801",
                Bio = "As the system administrator, I ensure that our platform runs smoothly, securely, and efficiently. From managing users and listings to maintaining system integrity, I'm here to support both guests and hosts for a seamless experience.",
                CountryId = 64,
                BirthDate = new DateOnly(1995, 5, 2)

            },
            new UserProfile
            {
               UserId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                FirstName = "Pavel",
                LastName = "Elmo",
                PhotoId ="98b7dcb6-7c53-4216-9f7a-259f40371fd4",
                Bio = "Hi, I’m Pavel! I’ve been hosting guests from around the world for over 3 years. I love sharing my cozy home and local tips to help you experience the best of the city. Your comfort and privacy are my top priorities—feel free to reach out with any questions before or during your stay!",
                CountryId = 64,
                BirthDate = new DateOnly(1999, 12, 2)

            },
            
            new UserProfile
            {
               UserId = "4dacdb51-fee9-4479-904c-cafe7dca22a8",
                FirstName = "lucas",
                LastName = "Martin",
                PhotoId ="4ae9e354-5eac-4f3a-a4b3-7c84c5b31d89",
                Bio = "Hi, I’m Lucas! I enjoy exploring new cities, meeting new people, and experiencing different cultures. I’m a respectful guest who values comfort and cleanliness. Looking forward to staying in your wonderful property and making the most of my travels!",


                CountryId = 64,
                BirthDate = new DateOnly(2001, 2, 2)




},
            
  };
    }
}
