using Travellin.Core.Entities;

namespace Travellin.Infrastructure.Data.Seeds
{
    static class PropertyAvailabilitySeed
    {
        public static List<PropertyAvailability> Data => new()
        {
            new PropertyAvailability { Id = 1, StartDate = new DateTime(2025, 9, 2), EndDate = new DateTime(2025, 9, 6), PropertyId = "cc4e48ea-ca54-4d32-a448-3c2c9d14f936", IsAvailable = true },
            new PropertyAvailability { Id = 2, StartDate = new DateTime(2025, 8, 25), EndDate = new DateTime(2025, 8, 28), PropertyId = "cc4e48ea-ca54-4d32-a448-3c2c9d14f936", IsAvailable = true },
            new PropertyAvailability { Id = 3, StartDate = new DateTime(2025, 8, 22), EndDate = new DateTime(2025, 8, 30), PropertyId = "cc4e48ea-ca54-4d32-a448-3c2c9d14f936", IsAvailable = true },

            new PropertyAvailability { Id = 4, StartDate = new DateTime(2025, 8, 2), EndDate = new DateTime(2025, 8, 4), PropertyId = "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4", IsAvailable = true },
            new PropertyAvailability { Id = 5, StartDate = new DateTime(2025, 8, 11), EndDate = new DateTime(2025, 8, 12), PropertyId = "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4", IsAvailable = true },
            new PropertyAvailability { Id = 14, StartDate = new DateTime(2025, 9, 15), EndDate = new DateTime(2025, 9, 20), PropertyId = "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4", IsAvailable = true },

            new PropertyAvailability { Id = 6, StartDate = new DateTime(2025, 9, 22), EndDate = new DateTime(2025, 9, 30), PropertyId = "3e7f99ab-228a-4d90-91c4-6adf8c12e048", IsAvailable = true },
            new PropertyAvailability { Id = 7, StartDate = new DateTime(2025, 8, 2), EndDate = new DateTime(2025, 8, 6), PropertyId = "3e7f99ab-228a-4d90-91c4-6adf8c12e048", IsAvailable = true },
            new PropertyAvailability { Id = 15, StartDate = new DateTime(2025, 10, 1), EndDate = new DateTime(2025, 10, 10), PropertyId = "3e7f99ab-228a-4d90-91c4-6adf8c12e048", IsAvailable = true },

            new PropertyAvailability { Id = 8, StartDate = new DateTime(2025, 12, 10), EndDate = new DateTime(2025, 12, 16), PropertyId = "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa", IsAvailable = true },
            new PropertyAvailability { Id = 16, StartDate = new DateTime(2025, 12, 1), EndDate = new DateTime(2025, 12, 7), PropertyId = "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa", IsAvailable = true },
            new PropertyAvailability { Id = 17, StartDate = new DateTime(2025, 12, 15), EndDate = new DateTime(2025, 12, 22), PropertyId = "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa", IsAvailable = true },

            new PropertyAvailability { Id = 9, StartDate = new DateTime(2025, 8, 22), EndDate = new DateTime(2025, 8, 30), PropertyId = "4e3d342-8e8d-4f1d-8123-2d09cb92b6a2", IsAvailable = true },
            new PropertyAvailability { Id = 10, StartDate = new DateTime(2025, 8, 2), EndDate = new DateTime(2025, 8, 6), PropertyId = "4e3d342-8e8d-4f1d-8123-2d09cb92b6a2", IsAvailable = true },
            new PropertyAvailability { Id = 18, StartDate = new DateTime(2025, 12, 20), EndDate = new DateTime(2025, 12, 31), PropertyId = "4e3d342-8e8d-4f1d-8123-2d09cb92b6a2", IsAvailable = true },

            new PropertyAvailability { Id = 11, StartDate = new DateTime(2025, 8, 10), EndDate = new DateTime(2025, 8, 14), PropertyId = "a43ecbfa-7b0a-4f6b-9c88-987be3c4e3d3", IsAvailable = true },
            new PropertyAvailability { Id = 19, StartDate = new DateTime(2025, 9, 5), EndDate = new DateTime(2025, 9, 12), PropertyId = "a43ecbfa-7b0a-4f6b-9c88-987be3c4e3d3", IsAvailable = true },
            new PropertyAvailability { Id = 20, StartDate = new DateTime(2025, 10, 20), EndDate = new DateTime(2025, 10, 27), PropertyId = "a43ecbfa-7b0a-4f6b-9c88-987be3c4e3d3", IsAvailable = true },

            new PropertyAvailability { Id = 12, StartDate = new DateTime(2025, 8, 22), EndDate = new DateTime(2025, 8, 30), PropertyId = "f1cc1b4c-b674-4a1a-89ee-5f7b4d44d2f7", IsAvailable = true },
            new PropertyAvailability { Id = 21, StartDate = new DateTime(2025, 11, 10), EndDate = new DateTime(2025, 11, 20), PropertyId = "f1cc1b4c-b674-4a1a-89ee-5f7b4d44d2f7", IsAvailable = true },
            new PropertyAvailability { Id = 22, StartDate = new DateTime(2025, 12, 5), EndDate = new DateTime(2025, 12, 15), PropertyId = "f1cc1b4c-b674-4a1a-89ee-5f7b4d44d2f7", IsAvailable = true },

            new PropertyAvailability { Id = 13, StartDate = new DateTime(2025, 8, 18), EndDate = new DateTime(2025, 8, 25), PropertyId = "d8eecb1f-5583-4d64-a7dc-5aef5e2c498f", IsAvailable = true },
            new PropertyAvailability { Id = 23, StartDate = new DateTime(2025, 11, 1), EndDate = new DateTime(2025, 11, 10), PropertyId = "d8eecb1f-5583-4d64-a7dc-5aef5e2c498f", IsAvailable = true },
            new PropertyAvailability { Id = 24, StartDate = new DateTime(2025, 12, 15), EndDate = new DateTime(2025, 12, 25), PropertyId = "d8eecb1f-5583-4d64-a7dc-5aef5e2c498f", IsAvailable = true },

            new PropertyAvailability { Id = 25, StartDate = new DateTime(2025, 9, 1), EndDate = new DateTime(2025, 9, 10), PropertyId = "4b04a76a-1608-4a8f-b09c-8d9043b83e16", IsAvailable = true },
            new PropertyAvailability { Id = 26, StartDate = new DateTime(2025, 10, 15), EndDate = new DateTime(2025, 10, 25), PropertyId = "4b04a76a-1608-4a8f-b09c-8d9043b83e16", IsAvailable = true },
            new PropertyAvailability { Id = 27, StartDate = new DateTime(2025, 11, 5), EndDate = new DateTime(2025, 11, 15), PropertyId = "4b04a76a-1608-4a8f-b09c-8d9043b83e16", IsAvailable = true },

            new PropertyAvailability { Id = 28, StartDate = new DateTime(2025, 8, 15), EndDate = new DateTime(2025, 8, 25), PropertyId = "2ab6e4d1-79b9-4dba-9109-22ef75a29ff1", IsAvailable = true },
            new PropertyAvailability { Id = 29, StartDate = new DateTime(2025, 9, 10), EndDate = new DateTime(2025, 9, 20), PropertyId = "2ab6e4d1-79b9-4dba-9109-22ef75a29ff1", IsAvailable = true },
            new PropertyAvailability { Id = 30, StartDate = new DateTime(2025, 10, 5), EndDate = new DateTime(2025, 10, 15), PropertyId = "2ab6e4d1-79b9-4dba-9109-22ef75a29ff1", IsAvailable = true },

            new PropertyAvailability { Id = 31, StartDate = new DateTime(2025, 8, 20), EndDate = new DateTime(2025, 8, 30), PropertyId = "ef3b2df2-e539-4cb9-8eb6-4eeb833e694c", IsAvailable = true },
            new PropertyAvailability { Id = 32, StartDate = new DateTime(2025, 9, 15), EndDate = new DateTime(2025, 9, 25), PropertyId = "ef3b2df2-e539-4cb9-8eb6-4eeb833e694c", IsAvailable = true },
            new PropertyAvailability { Id = 33, StartDate = new DateTime(2025, 10, 10), EndDate = new DateTime(2025, 10, 20), PropertyId = "ef3b2df2-e539-4cb9-8eb6-4eeb833e694c", IsAvailable = true },

            new PropertyAvailability { Id = 34, StartDate = new DateTime(2025, 9, 1), EndDate = new DateTime(2025, 9, 10), PropertyId = "3c0e361a-51df-4e03-b8d0-2d7601aa60f6", IsAvailable = true },
            new PropertyAvailability { Id = 35, StartDate = new DateTime(2025, 10, 5), EndDate = new DateTime(2025, 10, 15), PropertyId = "3c0e361a-51df-4e03-b8d0-2d7601aa60f6", IsAvailable = true },
            new PropertyAvailability { Id = 36, StartDate = new DateTime(2025, 11, 20), EndDate = new DateTime(2025, 11, 30), PropertyId = "3c0e361a-51df-4e03-b8d0-2d7601aa60f6", IsAvailable = true },

            new PropertyAvailability { Id = 37, StartDate = new DateTime(2025, 8, 10), EndDate = new DateTime(2025, 8, 20), PropertyId = "c5c0d4db-b048-4ee4-8835-344900fd35b2", IsAvailable = true },
            new PropertyAvailability { Id = 38, StartDate = new DateTime(2025, 9, 15), EndDate = new DateTime(2025, 9, 25), PropertyId = "c5c0d4db-b048-4ee4-8835-344900fd35b2", IsAvailable = true },
            new PropertyAvailability { Id = 39, StartDate = new DateTime(2025, 10, 1), EndDate = new DateTime(2025, 10, 10), PropertyId = "c5c0d4db-b048-4ee4-8835-344900fd35b2", IsAvailable = true },


            new PropertyAvailability { Id = 40, StartDate = new DateTime(2025, 8, 5), EndDate = new DateTime(2025, 8, 15), PropertyId = "0bb50f31-e322-4b76-97dd-6a7fcf585d33", IsAvailable = true },
            new PropertyAvailability { Id = 41, StartDate = new DateTime(2025, 9, 10), EndDate = new DateTime(2025, 9, 20), PropertyId = "0bb50f31-e322-4b76-97dd-6a7fcf585d33", IsAvailable = true },
            new PropertyAvailability { Id = 42, StartDate = new DateTime(2025, 10, 5), EndDate = new DateTime(2025, 10, 15), PropertyId = "0bb50f31-e322-4b76-97dd-6a7fcf585d33", IsAvailable = true },

            new PropertyAvailability { Id = 43, StartDate = new DateTime(2025, 8, 15), EndDate = new DateTime(2025, 8, 25), PropertyId = "a555515a-ff8a-4741-b0a4-db9be729198e", IsAvailable = true },
            new PropertyAvailability { Id = 44, StartDate = new DateTime(2025, 9, 5), EndDate = new DateTime(2025, 9, 15), PropertyId = "a555515a-ff8a-4741-b0a4-db9be729198e", IsAvailable = true },
            new PropertyAvailability { Id = 45, StartDate = new DateTime(2025, 10, 20), EndDate = new DateTime(2025, 10, 30), PropertyId = "a555515a-ff8a-4741-b0a4-db9be729198e", IsAvailable = true },


            new PropertyAvailability { Id = 46, StartDate = new DateTime(2025, 8, 1), EndDate = new DateTime(2025, 8, 10), PropertyId = "c10d2d46-869a-46bc-a46d-90bdd958c252", IsAvailable = true },
            new PropertyAvailability { Id = 47, StartDate = new DateTime(2025, 9, 15), EndDate = new DateTime(2025, 9, 25), PropertyId = "c10d2d46-869a-46bc-a46d-90bdd958c252", IsAvailable = true },
            new PropertyAvailability { Id = 48, StartDate = new DateTime(2025, 10, 10), EndDate = new DateTime(2025, 10, 20), PropertyId = "c10d2d46-869a-46bc-a46d-90bdd958c252", IsAvailable = true },

            new PropertyAvailability { Id = 49, StartDate = new DateTime(2025, 8, 10), EndDate = new DateTime(2025, 8, 20), PropertyId = "1adca40b-b8ff-4cea-b6e4-8e5f40d29c08", IsAvailable = true },
            new PropertyAvailability { Id = 50, StartDate = new DateTime(2025, 9, 5), EndDate = new DateTime(2025, 9, 15), PropertyId = "1adca40b-b8ff-4cea-b6e4-8e5f40d29c08", IsAvailable = true },
            new PropertyAvailability { Id = 51, StartDate = new DateTime(2025, 10, 20), EndDate = new DateTime(2025, 10, 30), PropertyId = "1adca40b-b8ff-4cea-b6e4-8e5f40d29c08", IsAvailable = true },

            new PropertyAvailability { Id = 52, StartDate = new DateTime(2025, 8, 15), EndDate = new DateTime(2025, 8, 25), PropertyId = "294e2751-203b-4beb-b21e-0bb96f082d7c", IsAvailable = true },
            new PropertyAvailability { Id = 53, StartDate = new DateTime(2025, 9, 10), EndDate = new DateTime(2025, 9, 20), PropertyId = "294e2751-203b-4beb-b21e-0bb96f082d7c", IsAvailable = true },
            new PropertyAvailability { Id = 54, StartDate = new DateTime(2025, 10, 5), EndDate = new DateTime(2025, 10, 15), PropertyId = "294e2751-203b-4beb-b21e-0bb96f082d7c", IsAvailable = true },

            new PropertyAvailability { Id = 55, StartDate = new DateTime(2025, 8, 20), EndDate = new DateTime(2025, 8, 30), PropertyId = "06dbae08-bc6b-4ca6-9162-3213784b9971", IsAvailable = true },
            new PropertyAvailability { Id = 56, StartDate = new DateTime(2025, 9, 15), EndDate = new DateTime(2025, 9, 25), PropertyId = "06dbae08-bc6b-4ca6-9162-3213784b9971", IsAvailable = true },
            new PropertyAvailability { Id = 57, StartDate = new DateTime(2025, 10, 10), EndDate = new DateTime(2025, 10, 20), PropertyId = "06dbae08-bc6b-4ca6-9162-3213784b9971", IsAvailable = true },


            new PropertyAvailability { Id = 58, StartDate = new DateTime(2025, 8, 1), EndDate = new DateTime(2025, 8, 10), PropertyId = "f1e8be41-4fd5-47e4-8960-12d8f4afc273", IsAvailable = true },
            new PropertyAvailability { Id = 59, StartDate = new DateTime(2025, 9, 5), EndDate = new DateTime(2025, 9, 15), PropertyId = "f1e8be41-4fd5-47e4-8960-12d8f4afc273", IsAvailable = true },
            new PropertyAvailability { Id = 60, StartDate = new DateTime(2025, 10, 20), EndDate = new DateTime(2025, 10, 30), PropertyId = "f1e8be41-4fd5-47e4-8960-12d8f4afc273", IsAvailable = true },


            new PropertyAvailability { Id = 61, StartDate = new DateTime(2025, 8, 10), EndDate = new DateTime(2025, 8, 20), PropertyId = "763e6c5f-1ad1-4071-b0e6-55e924624198", IsAvailable = true },
            new PropertyAvailability { Id = 62, StartDate = new DateTime(2025, 9, 15), EndDate = new DateTime(2025, 9, 25), PropertyId = "763e6c5f-1ad1-4071-b0e6-55e924624198", IsAvailable = true },
            new PropertyAvailability { Id = 63, StartDate = new DateTime(2025, 10, 1), EndDate = new DateTime(2025, 10, 10), PropertyId = "763e6c5f-1ad1-4071-b0e6-55e924624198", IsAvailable = true },


            new PropertyAvailability { Id = 64, StartDate = new DateTime(2025, 8, 15), EndDate = new DateTime(2025, 8, 25), PropertyId = "efd964ab-dceb-4b96-b113-665c5684a102", IsAvailable = true },
            new PropertyAvailability { Id = 65, StartDate = new DateTime(2025, 9, 10), EndDate = new DateTime(2025, 9, 20), PropertyId = "efd964ab-dceb-4b96-b113-665c5684a102", IsAvailable = true },
            new PropertyAvailability { Id = 66, StartDate = new DateTime(2025, 10, 5), EndDate = new DateTime(2025, 10, 15), PropertyId = "efd964ab-dceb-4b96-b113-665c5684a102", IsAvailable = true },


            new PropertyAvailability { Id = 67, StartDate = new DateTime(2025, 8, 20), EndDate = new DateTime(2025, 8, 30), PropertyId = "52a8df7d-c0b2-4ee3-8369-9daed4885f9f", IsAvailable = true },
            new PropertyAvailability { Id = 68, StartDate = new DateTime(2025, 9, 15), EndDate = new DateTime(2025, 9, 25), PropertyId = "52a8df7d-c0b2-4ee3-8369-9daed4885f9f", IsAvailable = true },
            new PropertyAvailability { Id = 69, StartDate = new DateTime(2025, 10, 10), EndDate = new DateTime(2025, 10, 20), PropertyId = "52a8df7d-c0b2-4ee3-8369-9daed4885f9f", IsAvailable = true },


            new PropertyAvailability { Id = 70, StartDate = new DateTime(2025, 8, 1), EndDate = new DateTime(2025, 8, 10), PropertyId = "c150e428-1c9a-43a2-be07-f4366875f1ce", IsAvailable = true },
            new PropertyAvailability { Id = 71, StartDate = new DateTime(2025, 9, 5), EndDate = new DateTime(2025, 9, 15), PropertyId = "c150e428-1c9a-43a2-be07-f4366875f1ce", IsAvailable = true },
            new PropertyAvailability { Id = 72, StartDate = new DateTime(2025, 10, 20), EndDate = new DateTime(2025, 10, 30), PropertyId = "c150e428-1c9a-43a2-be07-f4366875f1ce", IsAvailable = true },


            new PropertyAvailability { Id = 73, StartDate = new DateTime(2025, 8, 10), EndDate = new DateTime(2025, 8, 20), PropertyId = "2e3ed231-a2a6-4961-a1ba-f232d56c6f35", IsAvailable = true },
            new PropertyAvailability { Id = 74, StartDate = new DateTime(2025, 9, 15), EndDate = new DateTime(2025, 9, 25), PropertyId = "2e3ed231-a2a6-4961-a1ba-f232d56c6f35", IsAvailable = true },
            new PropertyAvailability { Id = 75, StartDate = new DateTime(2025, 10, 1), EndDate = new DateTime(2025, 10, 10), PropertyId = "2e3ed231-a2a6-4961-a1ba-f232d56c6f35", IsAvailable = true }
        };
    }
}