using Travellin.Core.Entities;

namespace Travellin.Infrastructure.Data.Seeds
{
    public static class PropertySpaceItemTypeSeed
    {
        public static List<PropertySpaceItemType> Data => new()
        {
            // 1. Bedroom
            new PropertySpaceItemType { Id = 1, Name = "Queen Bed", PropertySpaceTypeId = 1 },
            new PropertySpaceItemType { Id = 2, Name = "Nightstand", PropertySpaceTypeId = 1 },
            new PropertySpaceItemType { Id = 3, Name = "Wardrobe", PropertySpaceTypeId = 1 },

            // 2. Bathroom
            new PropertySpaceItemType { Id = 4, Name = "Shower", PropertySpaceTypeId = 2 },
            new PropertySpaceItemType { Id = 5, Name = "Bathtub", PropertySpaceTypeId = 2 },
            new PropertySpaceItemType { Id = 6, Name = "Toilet", PropertySpaceTypeId = 2 },

            // 3. Kitchen
            new PropertySpaceItemType { Id = 7, Name = "Stove", PropertySpaceTypeId = 3 },
            new PropertySpaceItemType { Id = 8, Name = "Refrigerator", PropertySpaceTypeId = 3 },
            new PropertySpaceItemType { Id = 9, Name = "Microwave", PropertySpaceTypeId = 3 },

            // 4. Living Room
            new PropertySpaceItemType { Id = 10, Name = "Sofa", PropertySpaceTypeId = 4 },
            new PropertySpaceItemType { Id = 11, Name = "Coffee Table", PropertySpaceTypeId = 4 },
            new PropertySpaceItemType { Id = 12, Name = "TV Stand", PropertySpaceTypeId = 4 },

            // 5. Dining Area
            new PropertySpaceItemType { Id = 13, Name = "Dining Table", PropertySpaceTypeId = 5 },
            new PropertySpaceItemType { Id = 14, Name = "Dining Chairs", PropertySpaceTypeId = 5 },
            new PropertySpaceItemType { Id = 15, Name = "Sideboard", PropertySpaceTypeId = 5 },

            // 6. Workspace
            new PropertySpaceItemType { Id = 16, Name = "Desk", PropertySpaceTypeId = 6 },
            new PropertySpaceItemType { Id = 17, Name = "Office Chair", PropertySpaceTypeId = 6 },
            new PropertySpaceItemType { Id = 18, Name = "Desk Lamp", PropertySpaceTypeId = 6 },

            // 7. Laundry Area
            new PropertySpaceItemType { Id = 19, Name = "Washer", PropertySpaceTypeId = 7 },
            new PropertySpaceItemType { Id = 20, Name = "Dryer", PropertySpaceTypeId = 7 },
            new PropertySpaceItemType { Id = 21, Name = "Ironing Board", PropertySpaceTypeId = 7 },

            // 8. Private Entrance
            new PropertySpaceItemType { Id = 22, Name = "Doormat", PropertySpaceTypeId = 8 },
            new PropertySpaceItemType { Id = 23, Name = "Shoe Rack", PropertySpaceTypeId = 8 },
            new PropertySpaceItemType { Id = 24, Name = "Umbrella Stand", PropertySpaceTypeId = 8 },

            // 9. Balcony
            new PropertySpaceItemType { Id = 25, Name = "Outdoor Chair", PropertySpaceTypeId = 9 },
            new PropertySpaceItemType { Id = 26, Name = "Small Table", PropertySpaceTypeId = 9 },
            new PropertySpaceItemType { Id = 27, Name = "Planter Box", PropertySpaceTypeId = 9 },

            // 10. Patio
            new PropertySpaceItemType { Id = 28, Name = "Patio Furniture", PropertySpaceTypeId = 10 },
            new PropertySpaceItemType { Id = 29, Name = "Grill", PropertySpaceTypeId = 10 },
            new PropertySpaceItemType { Id = 30, Name = "Shade Umbrella", PropertySpaceTypeId = 10 },

            // 11. Backyard
            new PropertySpaceItemType { Id = 31, Name = "Picnic Table", PropertySpaceTypeId = 11 },
            new PropertySpaceItemType { Id = 32, Name = "Garden Bench", PropertySpaceTypeId = 11 },
            new PropertySpaceItemType { Id = 33, Name = "Hammock", PropertySpaceTypeId = 11 },

            // 12. Fire Pit
            new PropertySpaceItemType { Id = 34, Name = "Fire Pit Ring", PropertySpaceTypeId = 12 },
            new PropertySpaceItemType { Id = 35, Name = "Seating", PropertySpaceTypeId = 12 },
            new PropertySpaceItemType { Id = 36, Name = "Log Holder", PropertySpaceTypeId = 12 },

            // 13. Baby Room
            new PropertySpaceItemType { Id = 37, Name = "Crib", PropertySpaceTypeId = 13 },
            new PropertySpaceItemType { Id = 38, Name = "Changing Table", PropertySpaceTypeId = 13 },
            new PropertySpaceItemType { Id = 39, Name = "Baby Monitor", PropertySpaceTypeId = 13 },

            // 14. Children’s Play Area
            new PropertySpaceItemType { Id = 40, Name = "Toy Box", PropertySpaceTypeId = 14 },
            new PropertySpaceItemType { Id = 41, Name = "Play Rug", PropertySpaceTypeId = 14 },
            new PropertySpaceItemType { Id = 42, Name = "Slide", PropertySpaceTypeId = 14 },

            // 15. Closet
            new PropertySpaceItemType { Id = 43, Name = "Hangers", PropertySpaceTypeId = 15 },
            new PropertySpaceItemType { Id = 44, Name = "Shelving Unit", PropertySpaceTypeId = 15 },
            new PropertySpaceItemType { Id = 45, Name = "Shoe Organizer", PropertySpaceTypeId = 15 },

            // 16. Storage Space
            new PropertySpaceItemType { Id = 46, Name = "Storage Boxes", PropertySpaceTypeId = 16 },
            new PropertySpaceItemType { Id = 47, Name = "Shelves", PropertySpaceTypeId = 16 },
            new PropertySpaceItemType { Id = 48, Name = "Plastic Bins", PropertySpaceTypeId = 16 },
        };
    }
}
