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
                FirstName = "John",
                LastName = "Doe",
                PhotoId ="c3d1f440-7e0e-4f38-8b5d-34ea8d12e801",
                Bio = "Hello, I'm John, the platform administrator. My role is to ensure all operations run smoothly and securely. I'm dedicated to providing a safe and reliable environment for our community. Feel free to reach out to our support team if you need any assistance!",
                CountryId = 100,
                BirthDate = new DateOnly(1988, 11, 15),
                Status = "Active"

            },
            new UserProfile
            {
                UserId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                FirstName = "David",
                LastName = "Lee",
                PhotoId ="98b7dcb6-7c53-4216-9f7a-259f40371fd4",
                Bio = "Welcome to my home! I'm David, a passionate traveler and host. I've been sharing my space for five years and love meeting people from all over. My goal is to make your stay as comfortable and memorable as possible. Don't hesitate to ask for recommendations on local cafes and hidden gems!",
                CountryId = 87,
                BirthDate = new DateOnly(1992, 7, 21),
                Status = "Active"

            },

            new UserProfile
            {
                UserId = "4dacdb51-fee9-4479-904c-cafe7dca22a8",
                FirstName = "Emily",
                LastName = "Jones",
                PhotoId ="4ae9e354-5eac-4f3a-a4b3-7c84c5b31d89",
                Bio = "Hi! I'm Emily, a digital nomad who loves exploring new places. I am a tidy and considerate guest who respects local culture and hospitality. I'm excited to experience the unique charm of your property and the surrounding area. Happy to connect with other travelers!",
                CountryId = 231,
                BirthDate = new DateOnly(1998, 4, 10),
                Status = "Active"
            }

        };
    }
}