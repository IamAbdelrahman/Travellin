using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travellin.Core.Entities;

namespace Travellin.Infrastructure.Data.Seeds
{
    
      

    static class PropertySeed
    {
        public static List<Property> Data => new()
        {
            new Property
            {
                Id = "cc4e48ea-ca54-4d32-a448-3c2c9d14f936",
                Title = "Entire rental unit in Nazlet El-Semman, Egypt",
                Description = "Enjoy your stay with Panoramic View of the giza pyramids and sphinx .Yes! view and pictures are all 100% real. (Be sure to check out our other listings too) Indulge in a stunning view of all the Giza Pyramids from anywhere within this contemporary oriental studio or while relaxing in the Jacuzzi. It is also a 10 min walk from the Pyramids entrance gate. To make the most of your trip, make sure to check out our experiences!We're committed to providing our guests the magical hospitality",
                PricePerNight = 100,
                LocationId = 1,
                Latitude = 29.98333m,
                Longitude = 31.13333m,
                SafteyInfo = "No Carbon monoxide alarm , No Smoke alarm ",
                HouseRules = "Check-in after 2:00 PM , Checkout before 11:00 AM , 2 guests maximum",
                CancellationPolicy = "Free cancellation before May 28 , Cancel before check-in on Jun 2 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 1
            },
            new Property
            {
                Id = "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4",
                Title = "Milano Duomo center 10 min Flat inside a castle",
                Description = "Elegant apartment inside the famous castle in Nolo, a royal choice right in the center of Milan A few steps away is the metro (M1 red for the Duomo 10 min), 10 minutes' walk for the central station. The apartment is well connected by trains, trams and buses The area is well supplied with restaurants, supermarkets, bars, clubs, etc. Complete comfort:82 Smart TV, Netflix, prime, wifi, dishwasher, kitchen, coffee machine The stay is included with a complete reception service",
                PricePerNight = 250,
                LocationId = 2,
                Latitude = 45.46427m,
                Longitude = 9.18951m,
                SafteyInfo = "Carbon monoxide alarm ,Smoke alarm installed",
                HouseRules = "Check-in: 3:00 PM - 11:00PM ,Checkout before 11:00 AM ,4 guests maximum",
                CancellationPolicy = "Free cancellation before May 17 , Cancel before check-in on May 18 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 3
            },
            new Property
            {
                Id = "3e7f99ab-228a-4d90-91c4-6adf8c12e048",
                Title = "Rent an apartment near Alhar Mecca",
                Description = "Relax with this listing Small 2-room 7-bed apartment near Alharam Al Makkah with a maximum of 10 to 12 minutes' walk away The ears and prayer are also heard inside the rooms and the window appears from the window of the Haram Al-Sharif .We offer a Surface kitchen with tea and coffee supplies, a mini fridge, a microwave, a water kettle and more A washing machine is available and we provide toiletries from towels, shampoo, lotion, soap, and more We provide a wheelchair ,wi-fi .This place is in a high tower where the apartment is located on the 17th floor Wish you a unique and pleasant stay",
                PricePerNight = 90,
                LocationId = 3,
                Latitude = 21.4266m,
                Longitude = 39.8256m,
                SafteyInfo = "Carbon monoxide alarm ,Smoke alarm installed",
                HouseRules = "Check-in after 3:00 PM , Checkout before 12:00 PM , 7 guests maximum",
                CancellationPolicy = "Free cancellation before May 17 , Cancel before check-in on May 18 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 1
            },
            new Property
            {
                Id = "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa",
                Title = "Hawkeye Dome - New Pool and Spa",
                Description = "Updated pool and spa! Sitting on 100 acres, Hawkeye House, featured on the cover of the May 2019 issue of Dwell Magazine, is an off grid Geodesic Dome. It has a 40 foot pool and hot tub that you will have to see to believe. This unique and modern home has been fully remodeled with an attention to both comfort and detail. Amazing hikes and privacy are abundant here. Most people never want to leave the property",
                PricePerNight = 110,
                LocationId = 4,
                Latitude = 34.114174m,
                Longitude = -116.432236m,
                SafteyInfo = "Carbon monoxide alarm ,Smoke alarm installed",
                HouseRules = "Check-in after 3:00 PM , Checkout before 12:00 PM , 7 guests maximum",
                CancellationPolicy = "Free cancellation before May 17 , Cancel before check-in on May 18 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 2
            },
            new Property
            {
                Id = "4e3d342-8e8d-4f1d-8123-2d09cb92b6a2",
                Title = "(4) charming oceanfront loft!",
                Description = "Romantic Loft with mezzanine and large balcony in front of the sea, double bed and 1 single bed, tv, wi-fi, fan, cabinet modern decoration, 180 degree terrace to the sea, equipped kitchen, bathroom, total comfort and privacy, fourth floor without elevator, 5 minutes from the carnival circuit, Noble Quarter of the city. Between the Surf and Paciencia beaches. Total security. The most beautiful sunset in Salvador",
                PricePerNight = 130,
                LocationId = 5,
                Latitude = -12.9711m,
                Longitude = -38.5108m,
                SafteyInfo = "Carbon monoxide alarm not reported , Smoke alarm not reported , Exterior security cameras on property",
                HouseRules = "3 guests maximum , Pets allowed",
                CancellationPolicy = "Free cancellation before Oct 22 , Cancel before check-in on Oct 23 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 1
            },
            new Property
            {
                Id = "a43ecbfa-7b0a-4f6b-9c88-987be3c4e3d3",
                Title = "Sunny and cozy Apartment Sagrada Familia",
                Description = "Set in an architectural prize-winning building, this modern Barcelona apartment beauty has impressive detail throughout. Ceiling-to-floor sloped windows, wood floor, and other soft designer textures accentuate this spectacular space. It is cozy and welcoming but with a very hip, urban edge.Design enthusiasts and those looking for that modern Barcelona feel will love the apartment. However, high-comfort and proximity to the Sagrada Familia suits all tastes.",
                PricePerNight = 310,
                LocationId = 6,
                Latitude = 41.3888m,
                Longitude = 2.159m,
                SafteyInfo = "No carbon monoxide alarm , No smoke alarm , Heights without rails or protectio",
                HouseRules = "Check-in: 3:00 PM - 5:00 PM ,Checkout before 10:00 AM ,2 guests maximum",
                CancellationPolicy = "Free cancellation before Jun 3. Cancel before check-in on Jun 4 for a partial refund",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 1
            },
            new Property
            {
                Id = "f1cc1b4c-b674-4a1a-89ee-5f7b4d44d2f7",
                Title = "Wadi Rum Sunset Cave",
                Description = "To give you the best experience of the authentic Bedouin life style, we will gather around the fire, cook our traditional food and tell you stories of our ancestors, while looking at the sky full of stars.Without a lie, this experience will be very special, if you used to cities and crowd in your everyday life.We created the space in a very simple, traditional and nomadic way. The Cave is inside the red rocks, waterproof and safe from all sides. Here you will have the whole Desert for yourself to get away from normal life, to relax, be in a quiet environment and meditate.",
                PricePerNight = 220,
                LocationId = 7,
                Latitude = 29.5726m,
                Longitude = 35.4186m,
                SafteyInfo = "No carbon monoxide alarm , No smoke alarm , Heights without rails or protectio",
                HouseRules = "Check-in: 3:00 PM - 5:00 PM ,Checkout before 10:00 AM ,2 guests maximum",
                CancellationPolicy = "Free cancellation before Jun 3. Cancel before check-in on Jun 4 for a partial refund",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 1
            },
            new Property
            {
                Id = "d8eecb1f-5583-4d64-a7dc-5aef5e2c498f",
                Title = "New! The View: See to Mouintain (with pool)",
                Description = "Interior designer's own guesthouse, this unique place has a style all its own. Escape the ordinary and immerse yourself in comfort, calm and luxury at our charming bergerie, a conversion from a shepherd's old stone house! Nestled in the heart of the largest mimosa forest in Europe, overlooking the Cotes d'Azur and lower Alps, our tastefully designed retreat offers everything you need for an unforgettable tranquillity.We welcome up to 4 adults and have a small mezzanine for children.",
                PricePerNight = 132,
                LocationId = 8,
                Latitude = 43.5914m,
                Longitude = 6.8761m,
                SafteyInfo = "No carbon monoxide alarm , No smoke alarm , Heights without rails or protectio",
                HouseRules = "Check-in: 3:00 PM - 5:00 PM ,Checkout before 10:00 AM ,2 guests maximum",
                CancellationPolicy = "Free cancellation before Jun 3. Cancel before check-in on Jun 4 for a partial refund",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 1
            },
            new Property
            {
                Id = "4b04a76a-1608-4a8f-b09c-8d9043b83e16",
                Title = "Moinho das Feteiras | The Mill House",
                Description = "Built in the 19th century, with a 360 degrees view over the sea and surroundings on the top floor.It features a Bedroom, a very well-decorated living room with kitchenette, and a WC.Free WiFi, air conditioning, Led TV and DVD player.Private parking inside the premises, providing extra security.Perfect for an unforgettable honeymoon experience.",
                PricePerNight = 200,
                LocationId = 9,
                Latitude = 37.7428m,
                Longitude = 25.6806m,
                SafteyInfo = "Climbing or play structure , Carbon monoxide alarmSmoke alarm",
                HouseRules = "Check-in: 3:00 PM - 5:00 PM ,Checkout before 10:00 AM ,2 guests maximum",
                CancellationPolicy = "Free cancellation for 48 hours , Cancel before Jan 13 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 4
            },
            new Property
            {
                Id = "2ab6e4d1-79b9-4dba-9109-22ef75a29ff1",
                Title = "Emotional healing accommodation in Icheon-si, near Seoul",
                Description = "This is a guitar-shaped country house located in Icheon, a ceramic art village. It is a private house with a spacious terrace on the 3rd floor of the Sera Guitar Culture Center, famous for its unique appearance in the Icheon Ceramic Art Village, which blends in very well with nature.",
                PricePerNight = 180,
                LocationId = 10,
                Latitude = 37.3154m,
                Longitude = 127.4052m,
                SafteyInfo = "Carbon monoxide alarm not reported , Smoke alarm , Must climb stairs",
                HouseRules = "Check-in: 3:00 PM - 12:00 AM  , Checkout before 11:00  AM , 2 guests maximum",
                CancellationPolicy = "Free cancellation before May 19. Cancel before check-in on May 24 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 1
            },
            new Property
            {
                Id = "ef3b2df2-e539-4cb9-8eb6-4eeb833e694c",
                Title = "Kai Cottage",
                Description = "This is a guitar-shaped country house located in Icheon, a ceramic art village. It is a private house with a spacious terrace on the 3rd floor of the Sera Guitar Culture Center, famous for its unique appearance in the Icheon Ceramic Art Village, which blends in very well with nature.",
                PricePerNight = 210,
                LocationId = 11,
                Latitude = 33.9249m,
                Longitude = 18.4241m,
                SafteyInfo = "Carbon monoxide alarm not reported , Smoke alarm , Must climb stairs",
                HouseRules = "Check-in: 3:00 PM - 12:00 AM  , Checkout before 11:00  AM , 2 guests maximum",
                CancellationPolicy = "Free cancellation before May 19. Cancel before check-in on May 24 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 1
            },
            new Property
            {
                Id = "3c0e361a-51df-4e03-b8d0-2d7601aa60f6",
                Title = "sunny, spacious, clean room in maadi, cairo..",
                Description = "Maadi is an uptown , green suburb with villas and gardens. My building is a five storey building . It is in a quiet area but a few minutes-walk away from Rd 9 where there are shops, cafes and restaurants. Everything you need is right here yet in 15 mins u can be in center of town.",
                PricePerNight = 100,
                LocationId = 12,
                Latitude = 29.9617m,
                Longitude = 31.2667m,
                SafteyInfo = "No carbon monoxide alarm , No smoke alarm ,Nearby lake, river, other body of water",
                HouseRules = "Flexible check-in , 2 guests maximum , No pets",
                CancellationPolicy = "Free cancellation before Jun 18. Cancel before check-in on Jun 23 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 2
            },
            new Property
            {
                Id = "c5c0d4db-b048-4ee4-8835-344900fd35b2",
                Title = "Heather Cottage - Beautiful Wetland Views",
                Description = "Charming small cottage situated on the edge of wetlands with beautiful views. Private gazebo with covered firepit and a dock over looking the large pond. Located on our 5 acre free range egg farm in Merville, BC. The pond is home to a family of beavers, bald eagles, blue heron and various birds. Private walking trail off the cottage and access to the One Spot Trail at the end of our private drive.",
                PricePerNight = 400,
                LocationId = 13,
                Latitude = 49.6876m,
                Longitude = 124.9936m,
                SafteyInfo = "Exterior security cameras on property ,Carbon monoxide alarm , Smoke alarm",
                HouseRules = "Check-in after 3:00 PM,Checkout before 11:00 AM,2 guests maximum",
                CancellationPolicy = "Add your trip dates to get the cancellation details for this stay.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 4
            },
            new Property
            {
                Id = "0bb50f31-e322-4b76-97dd-6a7fcf585d33",
                Title = "Escape To Our Beachfront Oasis | Private Beach",
                Description = "With panoramic water views, Delta Hotels by Marriott Virginia Beach Waterfront is an oasis on the shores of the breathtaking Chesapeake Bay.Thrill your palate with fresh oysters, fish, and coastal cuisine at our distinctive hotel restaurant, featuring inspiring water views.",
                PricePerNight = 90,
                LocationId = 14,
                Latitude = 37.5407m,
                Longitude = 77.436m,
                SafteyInfo = "Carbon monoxide alarm, Smoke alarm",
                HouseRules = "Check-in: 4:00 PM - 12:00 AM , Checkout before 11:00 AM ,4 guests maximum",
                CancellationPolicy = "Free cancellation before May 2, Cancel before check-in on May 3 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 3
            },
            new Property
            {
                Id = "a555515a-ff8a-4741-b0a4-db9be729198e",
                Title = "Sea View S2: Waterfront, Private Beach",
                Description = "Discover this luxury apartment in Gammarth, in the tourist area, with sea views and direct access to a private beach reserved for residents. The master suite includes a private bathroom, and a second bathroom is available",
                PricePerNight = 20,
                LocationId = 15,
                Latitude = 36.9475m,
                Longitude = 10.3036m,
                SafteyInfo = "Carbon monoxide alarm not reported , Smoke alarm not reported",
                HouseRules = "Check-in after 3:00 PM,4 guests maximum,Pets allowed",
                CancellationPolicy = "Free cancellation before May 4. Cancel before check-in on May 5 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 4
            },
            new Property
            {
                Id = "c10d2d46-869a-46bc-a46d-90bdd958c252",
                Title = "Cosy English cottage with beautiful garden",
                Description = "Warm and cosy cottage decorated with antique furniture, with a lovely garden. Perfect if you're looking for a relaxing stay in beautiful countryside. The bedroom windows have blackout blinds and the beds are very comfortable.",
                PricePerNight = 230,
                LocationId = 16,
                Latitude = 50.7236m,
                Longitude = 4.8694m,
                SafteyInfo = "No carbon monoxide alarm , Nearby lake- river- other body of water , Smoke alarm",
                HouseRules = "Check-in: (4:00 PM - 10:00 PM) , Checkout before 11:00 AM , 4 guests maximum",
                CancellationPolicy = "Free cancellation before May 9. Cancel before check-in on May 14 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 1
            },
            new Property
            {
                Id = "1adca40b-b8ff-4cea-b6e4-8e5f40d29c08",
                Title = "Palermo/Recoleta. Stylish room w/ensuite-bath & AC",
                Description = "Comfortable room, queen bed, bathroom in suite, with air conditioning. Excelent location, among Palermo and Recoleta neighborhoods, one block away from Santa Fe av and 2 blocks away from subway line D.",
                PricePerNight = 190,
                LocationId = 17,
                Latitude = 34.6037m,
                Longitude = 58.3816m,
                SafteyInfo = "No carbon monoxide alarm  ,No Smoke alarm",
                HouseRules = "Check-in brfore 4:00 Am , Checkout before 9:00 AM , 2 guests maximum",
                CancellationPolicy = "Free cancellation before May 26. Cancel before check-in on May 14 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 2
            },
            new Property
            {
                Id = "294e2751-203b-4beb-b21e-0bb96f082d7c",
                Title = "The Foundry. Luxury 2BR w/pool",
                Description = "Charming industrial character and premium homely comfort in the most desirable location. A leisurely stroll away from the shopping, dining & nightlife of Admiralty Way, Lekki Phase 1.Relax in the swimming pool or enjoy movies on satellite, Netflix or Amazon. Superfast optic-fibre broadband wi-fi. Uninterrupted 24/7 generator power back-up.",
                PricePerNight = 200,
                LocationId = 18,
                Latitude = 6.4367m,
                Longitude = 3.5244m,
                SafteyInfo = "carbon monoxide alarm  , Smoke alarm",
                HouseRules = "Check-in brfore 2:00 Am , Checkout before 9:00 AM , 3 guests maximum",
                CancellationPolicy = "Free cancellation before May 3. Cancel before check-in on May 14 for a full refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 1
            },
            new Property
            {
                Id = "06dbae08-bc6b-4ca6-9162-3213784b9971",
                Title = "TXoi Farmstay- Homefarm in the valley of Lam Thuong",
                Description = "Xoi Farmstay is located in a green valley of Lam Thuong in the North of Vietnam, about 250km from Hanoi and near to Hagiang and Sapa.This is a place for those who love nature, watching rice fields, exotic mountains, spring and waterfall, authentic local culture, good food, especially non touristy",
                PricePerNight = 100,
                LocationId = 19,
                Latitude = 21.05m,
                Longitude = 105.4333m,
                SafteyInfo = "carbon monoxide alarm  ,No Smoke alarm",
                HouseRules = "Check-in brfore 1:00 Am , Checkout before 11:00 AM , 1 guests maximum",
                CancellationPolicy = "Free cancellation before May 5. Cancel before check-in on May 9 for a full refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 4
            },
            new Property
            {
                Id = "f1e8be41-4fd5-47e4-8960-12d8f4afc273",
                Title = "Cosy flat in the heart of Dubai",
                Description = "Welcome to our brand new one-bedroom flat offering incredible views of Business Bay canal and the iconic Burj Khalifa.",
                PricePerNight = 400,
                LocationId = 20,
                Latitude = 25.2769m,
                Longitude = 55.2962m,
                SafteyInfo = "carbon monoxide alarm  , Smoke alarm",
                HouseRules = "Check-in brfore 1:00 Am , Checkout before 11:00 AM , 1 guests maximum",
                CancellationPolicy = "Free cancellation before May 5. Cancel before check-in on May 9 for a full refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 1
            },
            new Property
            {
                Id = "763e6c5f-1ad1-4071-b0e6-55e924624198",
                Title = "Atlas Mountains Riad Oussagou",
                Description = "Dar Ouassaggou's owner, Houssine, is a fluent English speaker and looks forward to welcoming you to his friendly guesthouse retreat in the Atlas Mountains, A Warm Welcome Awaits you at Dar Ouassaggou.It is a small comfortable guest house with 13 en suite rooms and balcony .",
                PricePerNight = 220,
                LocationId = 21,
                Latitude = 31.1333m,
                Longitude = 7.9167m,
                SafteyInfo = "No carbon monoxide alarm  , Smoke alarm",
                HouseRules = "Check-in brfore 11:00 Am , Checkout before 12:00 AM , 3 guests maximum",
                CancellationPolicy = "Free cancellation before May 5. Cancel before check-in on May 9 for a full refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 2
            },
            new Property
            {
                Id = "efd964ab-dceb-4b96-b113-665c5684a102",
                Title = "The most spectacular treehouse in Colombia.",
                Description = "Two hours from Bogotá on the Bogotá-Sasaima road, live the unique experience of staying in a tree eight meters high.Wake up to the chirping of birds and fall asleep to the sound of the stream below.Enjoy a five-star suite with all the amenities in the branches of the trees.The cabin has hot water, a mini-fridge, and the most spectacular view.",
                PricePerNight = 100,
                LocationId = 22,
                Latitude = 4.96705m,
                Longitude = -74.43512m,
                SafteyInfo = "carbon monoxide alarm  , No Smoke alarm , Nearby lake, river, other body of water",
                HouseRules = "Check-in brfore 3:00 PM , Checkout before 12:00 PM , 3 guests maximum",
                CancellationPolicy = "Free cancellation before Apr 26. Cancel before check-in on May 1 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 4
            },
            new Property
            {
                Id = "52a8df7d-c0b2-4ee3-8369-9daed4885f9f",
                Title = "Quite Get Away near by theCenter",
                Description = "Chill in a quite and fresh area only 3 min drive to Ubud center.Our villa located in the middle of rice field , offered you great experience.Friendly owner will assist you 24 hours by call to make sure you can enjoy the stay .Stay for 3 nights and you will get Free Traditional Balinese massage for 1 person for 60 min to complete the lazy days",
                PricePerNight = 110,
                LocationId = 23,
                Latitude = -8.5441m,
                Longitude = 115.3255m,
                SafteyInfo = "carbon monoxide alarm  , No Smoke alarm , Nearby lake, river, other body of water",
                HouseRules = "Check-in brfore 3:00 PM , Checkout before 12:00 PM , 3 guests maximum",
                CancellationPolicy = "Free cancellation before Apr 26. Cancel before check-in on May 1 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 1
            },
            new Property
            {
                Id = "c150e428-1c9a-43a2-be07-f4366875f1ce",
                Title = "[*Bright new Metro C penthouse*].",
                Description = "Elegant and spacious apartment on the 4th floor, designed and realized for 6 people.Totally renovated in February 2025.,Composed of 2 double bedrooms, 1 single bedroom and a sofa bed in the dining room.,2 bathrooms of which one inside the double room.It is possible to access the terrace from each room.",
                PricePerNight = 90,
                LocationId = 24,
                Latitude = 41.9028m,
                Longitude = 12.4964m,
                SafteyInfo = "carbon monoxide alarm  ,  Smoke alarm",
                HouseRules = "Check-in brfore 1:00 PM , Checkout before 10:00 PM , 2 guests maximum",
                CancellationPolicy = "Free cancellation before Apr 29. Cancel before check-in on May 1 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 3
            },
            new Property
            {
                Id = "2e3ed231-a2a6-4961-a1ba-f232d56c6f35",
                Title = "Inone Mucho Selection Hotel Deluxe Room B&B",
                Description = "You will feel special from the beginning to the end of your holiday at Inone Mucho Selection Hotel, located on the seafront with a private beach in one of the clearest bays of Asarlik.Our facility which is located 5 minutes drive away from Bodrum center and 5 minutes from Gumbet bar street by walk. You can have a pleasant time while sipping your cocktail at our Iconic Beach restaurant, accompanied by various events and DJ performances.",
                PricePerNight = 200,
                LocationId = 25,
                Latitude = 37.0383m,
                Longitude = 27.4292m,
                SafteyInfo = "carbon monoxide alarm  ,  Smoke alarm",
                HouseRules = "Check-in brfore 1:00 PM , Checkout before 10:00 PM , 2 guests maximum",
                CancellationPolicy = "Free cancellation before Apr 29. Cancel before check-in on May 1 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 2
            }
        };
    }
}

  
