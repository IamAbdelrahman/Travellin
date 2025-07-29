using Travellin.Core.Entities;

namespace Travellin.Infrastructure.Data.Seeds
{
    static class PropertyPhotoSeed
    {
        public static List<PropertyPhoto> Data => new()
            {
                // Property: cc4e48ea-ca54-4d32-a448-3c2c9d14f936 (Egypt) dn
                new PropertyPhoto { PhotoId = "b455bb0a-69a3-4024-b5fa-5a49323e58fd", PropertyId = "cc4e48ea-ca54-4d32-a448-3c2c9d14f936", TouchedAt = new DateTime(2025, 5, 10), QualityStatus = "HighQuality", QualityFeedback = "Photo is clear and well-lit." },
                new PropertyPhoto { PhotoId = "dc16e3d2-16ed-4ff5-b9c2-27a1e8b5ccbe", PropertyId = "cc4e48ea-ca54-4d32-a448-3c2c9d14f936", TouchedAt = new DateTime(2025, 5, 10), QualityStatus = "Blurry", QualityFeedback = "Image is blurry, please upload a clearer photo." },
                new PropertyPhoto { PhotoId = "4b0f81f1-9bc0-45c6-988e-1a4fd270b3e0", PropertyId = "cc4e48ea-ca54-4d32-a448-3c2c9d14f936", TouchedAt = new DateTime(2025, 5, 10), QualityStatus = "Dark", QualityFeedback = "Photo is too dark, try taking it in better lighting." },

                // Property: 8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4 (Milan)
                new PropertyPhoto { PhotoId = "2ac68b52-e7b6-4bb7-9f8e-49aa7f2b2b6c", PropertyId = "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4", TouchedAt = new DateTime(2025, 5, 10), QualityStatus = "HighQuality", QualityFeedback = "Excellent composition and clarity." },
                new PropertyPhoto { PhotoId = "69c6c01e-65b3-4cf7-bbc7-2e94272b658a", PropertyId = "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4", TouchedAt = new DateTime(2025, 5, 10), QualityStatus = "Irrelevant", QualityFeedback = "Photo does not show the property." },
                new PropertyPhoto { PhotoId = "95cde2b1-305e-4c13-9293-8c4c8f7c8b9f", PropertyId = "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4", TouchedAt = new DateTime(2025, 5, 10), QualityStatus = "HighQuality", QualityFeedback = "Photo is suitable for listing." },

                // Property: 3e7f99ab-228a-4d90-91c4-6adf8c12e048 (Mecca)
                new PropertyPhoto { PhotoId = "7a18064f-b6cb-4d58-a51b-0e8a74eac7a4", PropertyId = "3e7f99ab-228a-4d90-91c4-6adf8c12e048", TouchedAt = new DateTime(2025, 5, 10), QualityStatus = "HighQuality", QualityFeedback = "Photo is clear and attractive." },
                new PropertyPhoto { PhotoId = "4dfe3d56-2d34-4a6b-9cb5-f7a5a2dd8c28", PropertyId = "3e7f99ab-228a-4d90-91c4-6adf8c12e048", TouchedAt = new DateTime(2025, 5, 10), QualityStatus = "Blurry", QualityFeedback = "Please retake the photo for better clarity." },
                new PropertyPhoto { PhotoId = "6c54a231-b88f-409f-b5d5-170180930186", PropertyId = "3e7f99ab-228a-4d90-91c4-6adf8c12e048", TouchedAt = new DateTime(2025, 5, 10), QualityStatus = "HighQuality", QualityFeedback = "Good lighting and focus." },

                // Property: 5ca2f710-3c1f-4966-a924-7bcdf5ce57aa (Hawkeye Dome)
                new PropertyPhoto { PhotoId = "26d418bb-0f90-4f3c-b339-7dd5c31b5e99", PropertyId = "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa", TouchedAt = new DateTime(2025, 5, 10), QualityStatus = "HighQuality", QualityFeedback = "Photo is clear and well-composed." },
                new PropertyPhoto { PhotoId = "a4c0d40d-e90e-4b14-8a2a-5ac0212be9b1", PropertyId = "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa", TouchedAt = new DateTime(2025, 5, 10), QualityStatus = "Dark", QualityFeedback = "Increase brightness for better visibility." },
                new PropertyPhoto { PhotoId = "89f65612-5023-489e-9604-2f01074abf0c", PropertyId = "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa", TouchedAt = new DateTime(2025, 5, 10), QualityStatus = "HighQuality", QualityFeedback = "Photo is suitable for listing." },

                // ... (rest of the data remains unchanged, or you can add similar QualityStatus and QualityFeedback as above)
            };
    }
}