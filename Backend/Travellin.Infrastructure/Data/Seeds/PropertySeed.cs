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
                Title = "Pyramid View Oasis: Jacuzzi & 10-Min Walk to Giza!",
                Description = "Experience the magic of ancient Egypt from your private oasis! This contemporary oriental studio boasts breathtaking panoramic views of the Giza Pyramids and Sphinx, 100% real and as stunning as the pictures. Relax in your private jacuzzi with iconic vistas. Just a 10-minute walk to the Pyramids entrance. Explore our unique experiences to enhance your trip. We're dedicated to providing magical hospitality!",
                PricePerNight = 100,
                LocationId = 1,
                Latitude = 29.98333m,
                Longitude = 31.13333m,
                SafteyInfo = "No Carbon monoxide alarm, No Smoke alarm",
                HouseRules = "Check-in after 2:00 PM, Checkout before 11:00 AM, 2 guests maximum",
                CancellationPolicy = "Free cancellation before May 28, Cancel before check-in on Jun 2 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 1
            },
            new Property
            {
                Id = "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4",
                Title = "Milan Castle Apartment: Duomo 10 Mins, Central Hub!",
                Description = "Live like royalty in this elegant apartment within a famous Milanese castle! Located in vibrant Nolo, you're just steps from the M1 metro (10 mins to Duomo) and a 10-minute walk to Central Station. Excellent connections via trains, trams, and buses. Enjoy a neighborhood rich with restaurants, supermarkets, and nightlife. Featuring an 82\" Smart TV with Netflix/Prime, Wi-Fi, dishwasher, full kitchen, and coffee machine. Includes complete reception service for a comfortable stay.",
                PricePerNight = 250,
                LocationId = 2,
                Latitude = 45.46427m,
                Longitude = 9.18951m,
                SafteyInfo = "Carbon monoxide alarm, Smoke alarm installed",
                HouseRules = "Check-in: 3:00 PM - 11:00PM, Checkout before 11:00 AM, 4 guests maximum",
                CancellationPolicy = "Free cancellation before May 17, Cancel before check-in on May 18 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 3
            },
            new Property
            {
                Id = "3e7f99ab-228a-4d90-91c4-6adf8c12e048",
                Title = "Peaceful Mecca Retreat: Haram Views, 10-Min Walk!",
                Description = "Find serenity in this cozy 2-room apartment, a 10-12 minute walk from Al-Haram Al-Makkah. Listen to the call to prayer from your window, which offers a glimpse of Al-Haram Al-Sharif. Equipped with a surface kitchen (tea/coffee, mini-fridge, microwave, kettle), washing machine, and toiletries. Wheelchair accessible and free Wi-Fi. Located on the 17th floor of a high tower for a truly unique and pleasant stay.",
                PricePerNight = 90,
                LocationId = 3,
                Latitude = 21.4266m,
                Longitude = 39.8256m,
                SafteyInfo = "Carbon monoxide alarm, Smoke alarm installed",
                HouseRules = "Check-in after 3:00 PM, Checkout before 12:00 PM, 7 guests maximum",
                CancellationPolicy = "Free cancellation before May 17, Cancel before check-in on May 18 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 1
            },
            new Property
            {
                Id = "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa",
                Title = "Hawkeye Dome: Epic Glamping with New Pool & Spa!",
                Description = "Featured on Dwell Magazine's cover, the Hawkeye Dome offers an extraordinary off-grid experience on 100 sprawling acres. Immerse yourself in nature with an updated 40-foot pool and hot tub that are truly spectacular. This unique, fully remodeled geodesic dome blends modern design with ultimate comfort. Endless hiking and complete privacy await. You'll never want to leave!",
                PricePerNight = 110,
                LocationId = 4,
                Latitude = 34.114174m,
                Longitude = -116.432236m,
                SafteyInfo = "Carbon monoxide alarm, Smoke alarm installed",
                HouseRules = "Check-in after 3:00 PM, Checkout before 12:00 PM, 7 guests maximum",
                CancellationPolicy = "Free cancellation before May 17, Cancel before check-in on May 18 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 2
            },
            new Property
            {
                Id = "4e3d342-8e8d-4f1d-8123-2d09cb92b6a2",
                Title = "Charming Oceanfront Loft: Salvador Sunset Views!",
                Description = "Discover this romantic loft featuring a mezzanine and a large, 180-degree oceanfront balcony. Enjoy a double bed, single bed, TV, Wi-Fi, and fan, all within a modernly decorated space. The equipped kitchen and private bathroom offer total comfort. Located on the fourth floor (no elevator) in a noble quarter, 5 minutes from the carnival circuit, between Surf and Paciencia beaches. Experience the most beautiful sunsets in Salvador with total security.",
                PricePerNight = 130,
                LocationId = 5,
                Latitude = -12.9711m,
                Longitude = -38.5108m,
                SafteyInfo = "Carbon monoxide alarm not reported, Smoke alarm not reported, Exterior security cameras on property",
                HouseRules = "3 guests maximum, Pets allowed",
                CancellationPolicy = "Free cancellation before Oct 22, Cancel before check-in on Oct 23 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 1
            },
            new Property
            {
                Id = "a43ecbfa-7b0a-4f6b-9c88-987be3c4e3d3",
                Title = "Sunny Sagrada Familia Apartment: Modern Barcelona Gem!",
                Description = "Stay in a prize-winning architectural building with this stunning modern Barcelona apartment. Impressive details abound, from ceiling-to-floor sloped windows to rich wood floors and designer textures. This space is cozy yet boasts a very hip, urban edge. Perfect for design enthusiasts and those seeking a modern Barcelona experience. High comfort and proximity to Sagrada Familia make it ideal for all guests.",
                PricePerNight = 310,
                LocationId = 6,
                Latitude = 41.3888m,
                Longitude = 2.159m,
                SafteyInfo = "No carbon monoxide alarm, No smoke alarm, Heights without rails or protection",
                HouseRules = "Check-in: 3:00 PM - 5:00 PM, Checkout before 10:00 AM, 2 guests maximum",
                CancellationPolicy = "Free cancellation before Jun 3. Cancel before check-in on Jun 4 for a partial refund",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 1
            },
            new Property
            {
                Id = "f1cc1b4c-b674-4a1a-89ee-5f7b4d44d2f7",
                Title = "Wadi Rum Sunset Cave: Authentic Bedouin Stargazing!",
                Description = "Immerse yourself in authentic Bedouin life at our unique Wadi Rum Sunset Cave. Gather around the fire, enjoy traditional food, and hear ancestral stories under a sky full of stars. A truly special escape from city life, offering a quiet environment for relaxation and meditation. This simple, traditional cave, built into the red rocks, is waterproof and safe, providing you with the entire desert to yourself.",
                PricePerNight = 220,
                LocationId = 7,
                Latitude = 29.5726m,
                Longitude = 35.4186m,
                SafteyInfo = "No carbon monoxide alarm, No smoke alarm, Heights without rails or protection",
                HouseRules = "Check-in: 3:00 PM - 5:00 PM, Checkout before 10:00 AM, 2 guests maximum",
                CancellationPolicy = "Free cancellation before Jun 3. Cancel before check-in on Jun 4 for a partial refund",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 1
            },
            new Property
            {
                Id = "d8eecb1f-5583-4d64-a7dc-5aef5e2c498f",
                Title = "The View: Designer Guesthouse with Pool & Mountain Vistas!",
                Description = "Escape to 'The View,' an interior designer's guesthouse offering style and tranquility. This unique bergerie, a converted old stone shepherd's house, is nestled in Europe's largest mimosa forest with stunning views of the Cotes d'Azur and lower Alps. Tastefully designed for comfort and luxury, it provides everything for an unforgettable, tranquil escape. Accommodates up to 4 adults, with a small mezzanine for children.",
                PricePerNight = 132,
                LocationId = 8,
                Latitude = 43.5914m,
                Longitude = 6.8761m,
                SafteyInfo = "No carbon monoxide alarm, No smoke alarm, Heights without rails or protection",
                HouseRules = "Check-in: 3:00 PM - 5:00 PM, Checkout before 10:00 AM, 2 guests maximum",
                CancellationPolicy = "Free cancellation before Jun 3. Cancel before check-in on Jun 4 for a partial refund",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 1
            },
            new Property
            {
                Id = "4b04a76a-1608-4a8f-b09c-8d9043b83e16",
                Title = "The Mill House: Romantic 19th-Century Sea View Retreat!",
                Description = "Step back in time at Moinho das Feteiras, a beautifully restored 19th-century mill house with a 360-degree sea and surrounding view from the top floor. This charming retreat features a cozy bedroom, a well-decorated living room with a kitchenette, and a WC. Enjoy modern comforts with free WiFi, air conditioning, LED TV, and DVD player. Private parking offers extra security. Perfect for an unforgettable honeymoon!",
                PricePerNight = 200,
                LocationId = 9,
                Latitude = 37.7428m,
                Longitude = 25.6806m,
                SafteyInfo = "Climbing or play structure, Carbon monoxide alarm, Smoke alarm",
                HouseRules = "Check-in: 3:00 PM - 5:00 PM, Checkout before 10:00 AM, 2 guests maximum",
                CancellationPolicy = "Free cancellation for 48 hours, Cancel before Jan 13 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 4
            },
            new Property
            {
                Id = "2ab6e4d1-79b9-4dba-9109-22ef75a29ff1",
                Title = "Unique Guitar House: Emotional Healing in Icheon-si, Korea!",
                Description = "Discover a truly unique stay at this guitar-shaped country house in Icheon, a renowned ceramic art village. This private retreat, featuring a spacious terrace on the 3rd floor of the Sera Guitar Culture Center, blends seamlessly with nature. Perfect for emotional healing and a memorable escape near Seoul.",
                PricePerNight = 180,
                LocationId = 10,
                Latitude = 37.3154m,
                Longitude = 127.4052m,
                SafteyInfo = "Carbon monoxide alarm not reported, Smoke alarm, Must climb stairs",
                HouseRules = "Check-in: 3:00 PM - 12:00 AM, Checkout before 11:00 AM, 2 guests maximum",
                CancellationPolicy = "Free cancellation before May 19. Cancel before check-in on May 24 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 1
            },
            new Property
            {
                Id = "ef3b2df2-e539-4cb9-8eb6-4eeb833e694c",
                Title = "Kai Cottage: Serene Getaway with Nature Views!",
                Description = "Unwind at Kai Cottage, a tranquil escape perfectly situated to offer stunning nature views. This private haven provides a spacious terrace and a serene atmosphere, ideal for relaxation and rejuvenation. Experience peace and quiet in a beautiful setting.",
                PricePerNight = 210,
                LocationId = 11,
                Latitude = 33.9249m,
                Longitude = 18.4241m,
                SafteyInfo = "Carbon monoxide alarm not reported, Smoke alarm, Must climb stairs",
                HouseRules = "Check-in: 3:00 PM - 12:00 AM, Checkout before 11:00 AM, 2 guests maximum",
                CancellationPolicy = "Free cancellation before May 19. Cancel before check-in on May 24 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 1
            },
            new Property
            {
                Id = "3c0e361a-51df-4e03-b8d0-2d7601aa60f6",
                Title = "Sunny Maadi Room: Cairo Charm, Steps from Cafes!",
                Description = "Discover a sunny, spacious, and clean room in the heart of Maadi, Cairo's upscale, green suburb. Nestled in a quiet area, this five-story building is just minutes from Road 9, offering an abundance of shops, cafes, and restaurants. Enjoy the perfect blend of tranquility and urban convenience, with downtown just a 15-minute ride away.",
                PricePerNight = 100,
                LocationId = 12,
                Latitude = 29.9617m,
                Longitude = 31.2667m,
                SafteyInfo = "No carbon monoxide alarm, No smoke alarm, Nearby lake, river, other body of water",
                HouseRules = "Flexible check-in, 2 guests maximum, No pets",
                CancellationPolicy = "Free cancellation before Jun 18. Cancel before check-in on Jun 23 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 2
            },
            new Property
            {
                Id = "c5c0d4db-b048-4ee4-8835-344900fd35b2",
                Title = "Heather Cottage: Wetland Views, Firepit & Farm Charm!",
                Description = "Experience the tranquility of Heather Cottage, a charming small retreat on the edge of picturesque wetlands. Enjoy stunning views, a private gazebo with a covered firepit, and a dock overlooking a large pond. Located on our 5-acre free-range egg farm in Merville, BC, the pond is home to beavers, bald eagles, and blue herons. Explore a private walking trail and easy access to the One Spot Trail.",
                PricePerNight = 400,
                LocationId = 13,
                Latitude = 49.6876m,
                Longitude = 124.9936m,
                SafteyInfo = "Exterior security cameras on property, Carbon monoxide alarm, Smoke alarm",
                HouseRules = "Check-in after 3:00 PM, Checkout before 11:00 AM, 2 guests maximum",
                CancellationPolicy = "Add your trip dates to get the cancellation details for this stay.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 4
            },
            new Property
            {
                Id = "0bb50f31-e322-4b76-97dd-6a7fcf585d33",
                Title = "Beachfront Oasis: Chesapeake Bay Views & Private Beach!",
                Description = "Indulge in an unforgettable escape at this beachfront oasis, the Delta Hotels by Marriott Virginia Beach Waterfront. Perched on the stunning shores of Chesapeake Bay, this distinctive hotel offers panoramic water views and a private beach. Savor fresh oysters, fish, and coastal cuisine at our restaurant, all while enjoying inspiring bay vistas.",
                PricePerNight = 90,
                LocationId = 14,
                Latitude = 37.5407m,
                Longitude = 77.436m,
                SafteyInfo = "Carbon monoxide alarm, Smoke alarm",
                HouseRules = "Check-in: 4:00 PM - 12:00 AM, Checkout before 11:00 AM, 4 guests maximum",
                CancellationPolicy = "Free cancellation before May 2, Cancel before check-in on May 3 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 3
            },
            new Property
            {
                Id = "a555515a-ff8a-4741-b0a4-db9be729198e",
                Title = "Luxury Gammarth Apartment: Sea Views & Private Beach Access!",
                Description = "Discover unparalleled luxury in this exquisite apartment in Gammarth's vibrant tourist area. Boasting breathtaking sea views and direct access to a private residents-only beach. The master suite features a private bathroom, with an additional second bathroom for convenience. Experience coastal living at its finest.",
                PricePerNight = 20,
                LocationId = 15,
                Latitude = 36.9475m,
                Longitude = 10.3036m,
                SafteyInfo = "Carbon monoxide alarm not reported, Smoke alarm not reported",
                HouseRules = "Check-in after 3:00 PM, 4 guests maximum, Pets allowed",
                CancellationPolicy = "Free cancellation before May 4. Cancel before check-in on May 5 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 4
            },
            new Property
            {
                Id = "c10d2d46-869a-46bc-a46d-90bdd958c252",
                Title = "Charming English Cottage: Antiques & Beautiful Garden!",
                Description = "Step into a warm and cozy English cottage, tastefully adorned with antique furniture and surrounded by a lovely garden. Perfect for a relaxing countryside escape. Enjoy comfortable beds with blackout blinds for a peaceful night's sleep. Immerse yourself in the beauty of the serene surroundings.",
                PricePerNight = 230,
                LocationId = 16,
                Latitude = 50.7236m,
                Longitude = 4.8694m,
                SafteyInfo = "No carbon monoxide alarm, Nearby lake- river- other body of water, Smoke alarm",
                HouseRules = "Check-in: (4:00 PM - 10:00 PM), Checkout before 11:00 AM, 4 guests maximum",
                CancellationPolicy = "Free cancellation before May 9. Cancel before check-in on May 14 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 1
            },
            new Property
            {
                Id = "1adca40b-b8ff-4cea-b6e4-8e5f40d29c08",
                Title = "Palermo/Recoleta Chic: Stylish Room with Ensuite & AC!",
                Description = "Experience comfort and style in this room featuring a queen bed, ensuite bathroom, and air conditioning. Enjoy an excellent location, nestled between the vibrant Palermo and Recoleta neighborhoods. Just one block from Santa Fe Ave and two blocks from subway line D, putting Buenos Aires at your fingertips.",
                PricePerNight = 190,
                LocationId = 17,
                Latitude = 34.6037m,
                Longitude = 58.3816m,
                SafteyInfo = "No carbon monoxide alarm, No Smoke alarm",
                HouseRules = "Check-in before 4:00 AM, Checkout before 9:00 AM, 2 guests maximum",
                CancellationPolicy = "Free cancellation before May 26. Cancel before check-in on May 14 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 2
            },
            new Property
            {
                Id = "294e2751-203b-4beb-b21e-0bb96f082d7c",
                Title = "The Foundry: Luxe 2BR, Pool & Lekki Phase 1 Prime Location!",
                Description = "Discover charming industrial character and premium comfort at The Foundry, ideally located near the vibrant shopping, dining, and nightlife of Admiralty Way, Lekki Phase 1. Relax by the swimming pool or enjoy endless entertainment with satellite TV, Netflix, and Amazon. Benefit from superfast fiber-optic Wi-Fi and uninterrupted 24/7 generator power back-up.",
                PricePerNight = 200,
                LocationId = 18,
                Latitude = 6.4367m,
                Longitude = 3.5244m,
                SafteyInfo = "Carbon monoxide alarm, Smoke alarm",
                HouseRules = "Check-in before 2:00 AM, Checkout before 9:00 AM, 3 guests maximum",
                CancellationPolicy = "Free cancellation before May 3. Cancel before check-in on May 14 for a full refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 1
            },
            new Property
            {
                Id = "06dbae08-bc6b-4ca6-9162-3213784b9971",
                Title = "Xoi Farmstay: Authentic Valley Retreat near Hanoi!",
                Description = "Escape to Xoi Farmstay, nestled in the lush green valley of Lam Thuong in Northern Vietnam, just 250km from Hanoi and close to Ha Giang and Sapa. This is a haven for nature lovers, offering stunning rice fields, exotic mountains, springs, and waterfalls. Experience authentic local culture and delicious food in a truly non-touristy setting.",
                PricePerNight = 100,
                LocationId = 19,
                Latitude = 21.05m,
                Longitude = 105.4333m,
                SafteyInfo = "Carbon monoxide alarm, No Smoke alarm",
                HouseRules = "Check-in before 1:00 AM, Checkout before 11:00 AM, 1 guest maximum",
                CancellationPolicy = "Free cancellation before May 5. Cancel before check-in on May 9 for a full refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 4
            },
            new Property
            {
                Id = "f1e8be41-4fd5-47e4-8960-12d8f4afc273",
                Title = "Central Dubai Flat: Burj Khalifa Views & Business Bay!",
                Description = "Welcome to our brand new, cozy one-bedroom flat in the heart of Dubai! Enjoy incredible views of the bustling Business Bay canal and the iconic Burj Khalifa. Perfectly situated for both leisure and business travelers seeking a prime location and stunning vistas.",
                PricePerNight = 400,
                LocationId = 20,
                Latitude = 25.2769m,
                Longitude = 55.2962m,
                SafteyInfo = "Carbon monoxide alarm, Smoke alarm",
                HouseRules = "Check-in before 1:00 AM, Checkout before 11:00 AM, 1 guest maximum",
                CancellationPolicy = "Free cancellation before May 5. Cancel before check-in on May 9 for a full refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 1
            },
            new Property
            {
                Id = "763e6c5f-1ad1-4071-b0e6-55e924624198",
                Title = "Atlas Mountains Riad: Oussagou Guest House Retreat!",
                Description = "Experience a warm welcome at Dar Ouassaggou, a comfortable guesthouse retreat in the stunning Atlas Mountains. Owner Houssine, a fluent English speaker, looks forward to hosting you. This small, inviting guesthouse features 13 en-suite rooms, each with a balcony, offering a perfect escape into nature and local hospitality.",
                PricePerNight = 220,
                LocationId = 21,
                Latitude = 31.1333m,
                Longitude = 7.9167m,
                SafteyInfo = "No carbon monoxide alarm, Smoke alarm",
                HouseRules = "Check-in before 11:00 AM, Checkout before 12:00 AM, 3 guests maximum",
                CancellationPolicy = "Free cancellation before May 5. Cancel before check-in on May 9 for a full refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 2
            },
            new Property
            {
                Id = "efd964ab-dceb-4b96-b113-665c5684a102",
                Title = "Colombia's Most Spectacular Treehouse: 5-Star Nature Escape!",
                Description = "Just two hours from Bogotá, live an unparalleled experience in Colombia's most spectacular treehouse, perched eight meters high. Wake to birdsong, fall asleep to a gentle stream, and enjoy a five-star suite with hot water, a mini-fridge, and breathtaking views, all nestled within the tree branches. A truly unique natural retreat.",
                PricePerNight = 100,
                LocationId = 22,
                Latitude = 4.96705m,
                Longitude = -74.43512m,
                SafteyInfo = "Carbon monoxide alarm, No Smoke alarm, Nearby lake, river, other body of water",
                HouseRules = "Check-in before 3:00 PM, Checkout before 12:00 PM, 3 guests maximum",
                CancellationPolicy = "Free cancellation before Apr 26. Cancel before check-in on May 1 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 4
            },
            new Property
            {
                Id = "52a8df7d-c0b2-4ee3-8369-9daed4885f9f",
                Title = "Quiet Ubud Villa: Rice Fields & Balinese Massage!",
                Description = "Unwind in a serene and fresh area, just a 3-minute drive from Ubud center. Our villa is nestled amidst lush rice fields, offering a truly authentic experience. Your friendly host is available 24/7 to ensure a delightful stay. Book for 3 nights and receive a complimentary 60-minute traditional Balinese massage for one person, perfect for completing your lazy days!",
                PricePerNight = 110,
                LocationId = 23,
                Latitude = -8.5441m,
                Longitude = 115.3255m,
                SafteyInfo = "Carbon monoxide alarm, No Smoke alarm, Nearby lake, river, other body of water",
                HouseRules = "Check-in before 3:00 PM, Checkout before 12:00 PM, 3 guests maximum",
                CancellationPolicy = "Free cancellation before Apr 26. Cancel before check-in on May 1 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 1
            },
            new Property
            {
                Id = "c150e428-1c9a-43a2-be07-f4366875f1ce",
                Title = "Bright & New Rome Penthouse: Metro C Access, Sleeps 6!",
                Description = "Discover this elegant and spacious penthouse on the 4th floor, freshly renovated in February 2025 and designed to comfortably sleep 6 guests. Featuring two double bedrooms, one single bedroom, and a sofa bed in the dining room, plus two bathrooms (one en-suite). Enjoy direct terrace access from every room, and easy Metro C access for exploring Rome!",
                PricePerNight = 90,
                LocationId = 24,
                Latitude = 41.9028m,
                Longitude = 12.4964m,
                SafteyInfo = "Carbon monoxide alarm, Smoke alarm",
                HouseRules = "Check-in before 1:00 PM, Checkout before 10:00 PM, 2 guests maximum",
                CancellationPolicy = "Free cancellation before Apr 29. Cancel before check-in on May 1 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 3
            },
            new Property
            {
                Id = "2e3ed231-a2a6-4961-a1ba-f232d56c6f35",
                Title = "Bodrum Beachfront Hotel: Private Beach & DJ Nights!",
                Description = "Feel special from arrival to departure at Inone Mucho Selection Hotel, a beachfront haven with a private beach in one of Asarlik's clearest bays. Just a 5-minute drive from Bodrum center and a 5-minute walk from Gumbet bar street. Sip cocktails at our Iconic Beach restaurant, accompanied by events and DJ performances, for an unforgettable holiday.",
                PricePerNight = 200,
                LocationId = 25,
                Latitude = 37.0383m,
                Longitude = 27.4292m,
                SafteyInfo = "Carbon monoxide alarm, Smoke alarm",
                HouseRules = "Check-in before 1:00 PM, Checkout before 10:00 PM, 2 guests maximum",
                CancellationPolicy = "Free cancellation before Apr 29. Cancel before check-in on May 1 for a partial refund.",
                OwnerId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                PropertyTypeId = 2
            }
        };
    }
}