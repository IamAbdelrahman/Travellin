using Travellin.Core.Entities;

namespace Travellin.Infrastructure.Data.Seeds
{
    static class FavoritePropertySeed
    {
        public static List<FavoriteProperty> Data => new()
        {
            new FavoriteProperty { PropertyId = "cc4e48ea-ca54-4d32-a448-3c2c9d14f936", UserId = "4dacdb51-fee9-4479-904c-cafe7dca22a8" },
            new FavoriteProperty { PropertyId = "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4", UserId = "4dacdb51-fee9-4479-904c-cafe7dca22a8" },
            new FavoriteProperty { PropertyId = "3e7f99ab-228a-4d90-91c4-6adf8c12e048", UserId = "4dacdb51-fee9-4479-904c-cafe7dca22a8" },
            new FavoriteProperty { PropertyId = "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa", UserId = "4dacdb51-fee9-4479-904c-cafe7dca22a8" },
            new FavoriteProperty { PropertyId = "4e3d342-8e8d-4f1d-8123-2d09cb92b6a2", UserId = "4dacdb51-fee9-4479-904c-cafe7dca22a8" },
            new FavoriteProperty { PropertyId = "a43ecbfa-7b0a-4f6b-9c88-987be3c4e3d3", UserId = "4dacdb51-fee9-4479-904c-cafe7dca22a8" },
            new FavoriteProperty { PropertyId = "f1cc1b4c-b674-4a1a-89ee-5f7b4d44d2f7", UserId = "4dacdb51-fee9-4479-904c-cafe7dca22a8" },
            new FavoriteProperty { PropertyId = "d8eecb1f-5583-4d64-a7dc-5aef5e2c498f", UserId = "4dacdb51-fee9-4479-904c-cafe7dca22a8" }
        };
    }
}