using Travellin.Core.Entities;

namespace Travellin.Infrastructure.Data.Seeds
{
    static class PropertySpaceSeed
    {
        public static List<PropertySpace> Data => new()
        {
             new PropertySpace { Id = "daae3bd2-707e-4374-9b6c-5703f9789c7f", Name = "Bedroom", PropertySpaceTypeId = 1, PropertyId = "cc4e48ea-ca54-4d32-a448-3c2c9d14f936", IsShared = false },
    new PropertySpace { Id = "d20a85b2-4019-4714-a63e-e017b4be4e3e", Name = "Kitchen", PropertySpaceTypeId = 3, PropertyId = "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4", IsShared = true },
    new PropertySpace { Id = "325e05d6-cc5d-4140-b1bc-d96fc52d86b3", Name = "Living Room", PropertySpaceTypeId = 4, PropertyId = "3e7f99ab-228a-4d90-91c4-6adf8c12e048", IsShared = true },
    new PropertySpace { Id = "62b66c76-60d1-4b4b-8e97-bfd0338ea05a", Name = "Bathroom", PropertySpaceTypeId = 2, PropertyId = "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa", IsShared = false },
    new PropertySpace { Id = "b09ce60b-b66b-47df-9985-41d1e7f6b254", Name = "Balcony", PropertySpaceTypeId = 9, PropertyId = "4e3d342-8e8d-4f1d-8123-2d09cb92b6a2", IsShared = true },
    new PropertySpace { Id = "726d598e-c948-41b6-8cc3-c7e1aa4a51e4", Name = "Dining Room", PropertySpaceTypeId = 5, PropertyId = "a43ecbfa-7b0a-4f6b-9c88-987be3c4e3d3", IsShared = true },
    new PropertySpace { Id = "14a66729-9580-472b-9438-dfc7e2440c95", Name = "Office", PropertySpaceTypeId = 6, PropertyId = "f1cc1b4c-b674-4a1a-89ee-5f7b4d44d2f7", IsShared = false },
    new PropertySpace { Id = "f8c7fef3-70f4-4650-baa6-f93db77dfd92", Name = "Game Room", PropertySpaceTypeId = 14, PropertyId = "d8eecb1f-5583-4d64-a7dc-5aef5e2c498f", IsShared = true },
    new PropertySpace { Id = "c8f09e6f-8c82-4026-b3ec-23be0a378a56", Name = "Storage", PropertySpaceTypeId = 16, PropertyId = "4b04a76a-1608-4a8f-b09c-8d9043b83e16", IsShared = false },
    new PropertySpace { Id = "96f6a377-d586-44a2-acc7-fc45c10d999c", Name = "Library", PropertySpaceTypeId = 6, PropertyId = "2ab6e4d1-79b9-4dba-9109-22ef75a29ff1", IsShared = true },
    new PropertySpace { Id = "1cc7112f-3bb5-4265-8e0a-b305274c0410", Name = "Gym", PropertySpaceTypeId = 6, PropertyId = "ef3b2df2-e539-4cb9-8eb6-4eeb833e694c", IsShared = true },
    new PropertySpace { Id = "6c67a41a-8274-4ad0-864e-20fd4866b2d4", Name = "Theater", PropertySpaceTypeId = 15, PropertyId = "3c0e361a-51df-4e03-b8d0-2d7601aa60f6", IsShared = true },
    new PropertySpace { Id = "846b07ee-bb17-4c94-82df-99f1f7643ea3", Name = "Pantry", PropertySpaceTypeId = 11, PropertyId = "c5c0d4db-b048-4ee4-8835-344900fd35b2", IsShared = true },
    new PropertySpace { Id = "1954cfa5-9c89-41a7-a6be-41c71b34efc9", Name = "Sunroom", PropertySpaceTypeId = 12, PropertyId = "0bb50f31-e322-4b76-97dd-6a7fcf585d33", IsShared = false },
    new PropertySpace { Id = "c6ae89de-0d1a-4e5a-9230-8ef6617a3b53", Name = "Hallway", PropertySpaceTypeId = 10, PropertyId = "a555515a-ff8a-4741-b0a4-db9be729198e", IsShared = false },
    new PropertySpace { Id = "188b8c66-66fa-42a2-944c-4fd3f048250c", Name = "Closet", PropertySpaceTypeId = 13, PropertyId = "c10d2d46-869a-46bc-a46d-90bdd958c252", IsShared = false },
    new PropertySpace { Id = "49f23d20-c9ae-4a77-9734-1886d424cb77", Name = "Laundry Room", PropertySpaceTypeId = 7, PropertyId = "1adca40b-b8ff-4cea-b6e4-8e5f40d29c08", IsShared = true },
    new PropertySpace { Id = "5eb1c7e5-efb6-4b3c-983f-d278c1c086e7", Name = "Reception", PropertySpaceTypeId = 8, PropertyId = "294e2751-203b-4beb-b21e-0bb96f082d7c", IsShared = true },
    new PropertySpace { Id = "9f5f9e6e-0d79-41ad-86a1-06cbff2d0e92", Name = "Guest Room", PropertySpaceTypeId = 1, PropertyId = "06dbae08-bc6b-4ca6-9162-3213784b9971", IsShared = false },
    new PropertySpace { Id = "30baf72d-9d00-4f3e-9405-2261d6f0dd76", Name = "Study", PropertySpaceTypeId = 6, PropertyId = "f1e8be41-4fd5-47e4-8960-12d8f4afc273", IsShared = true },
    new PropertySpace { Id = "6b09c3a9-e319-45d0-a253-b5d6f4f9de3a", Name = "Porch", PropertySpaceTypeId = 9, PropertyId = "763e6c5f-1ad1-4071-b0e6-55e924624198", IsShared = true },
    new PropertySpace { Id = "d2e5f682-06d0-40e7-a1e7-002b958d8048", Name = "Workshop", PropertySpaceTypeId = 13, PropertyId = "efd964ab-dceb-4b96-b113-665c5684a102", IsShared = false },
    new PropertySpace { Id = "29ac2c68-b4b4-45b8-918a-fbdf11660d7e", Name = "Playroom", PropertySpaceTypeId = 14, PropertyId = "52a8df7d-c0b2-4ee3-8369-9daed4885f9f", IsShared = true },
    new PropertySpace { Id = "8cf76f1f-7f39-4d78-bcc7-2a2a34db54b3", Name = "Utility Room", PropertySpaceTypeId = 15, PropertyId = "c150e428-1c9a-43a2-be07-f4366875f1ce", IsShared = false }
};

    }
}