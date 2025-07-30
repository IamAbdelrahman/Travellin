using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Travellin.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PropertyGuests",
                keyColumns: new[] { "GuestTypeId", "PropertyId" },
                keyValues: new object[] { 1, "2ab6e4d1-79b9-4dba-9109-22ef75a29ff1" });

            migrationBuilder.DeleteData(
                table: "PropertyGuests",
                keyColumns: new[] { "GuestTypeId", "PropertyId" },
                keyValues: new object[] { 1, "3c0e361a-51df-4e03-b8d0-2d7601aa60f6" });

            migrationBuilder.DeleteData(
                table: "PropertyGuests",
                keyColumns: new[] { "GuestTypeId", "PropertyId" },
                keyValues: new object[] { 1, "3e7f99ab-228a-4d90-91c4-6adf8c12e048" });

            migrationBuilder.DeleteData(
                table: "PropertyGuests",
                keyColumns: new[] { "GuestTypeId", "PropertyId" },
                keyValues: new object[] { 1, "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa" });

            migrationBuilder.DeleteData(
                table: "PropertyGuests",
                keyColumns: new[] { "GuestTypeId", "PropertyId" },
                keyValues: new object[] { 1, "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4" });

            migrationBuilder.DeleteData(
                table: "PropertyGuests",
                keyColumns: new[] { "GuestTypeId", "PropertyId" },
                keyValues: new object[] { 1, "a43ecbfa-7b0a-4f6b-9c88-987be3c4e3d3" });

            migrationBuilder.DeleteData(
                table: "PropertyGuests",
                keyColumns: new[] { "GuestTypeId", "PropertyId" },
                keyValues: new object[] { 1, "d8eecb1f-5583-4d64-a7dc-5aef5e2c498f" });

            migrationBuilder.DeleteData(
                table: "PropertyGuests",
                keyColumns: new[] { "GuestTypeId", "PropertyId" },
                keyValues: new object[] { 1, "ef3b2df2-e539-4cb9-8eb6-4eeb833e694c" });

            migrationBuilder.DeleteData(
                table: "PropertyGuests",
                keyColumns: new[] { "GuestTypeId", "PropertyId" },
                keyValues: new object[] { 1, "f1cc1b4c-b674-4a1a-89ee-5f7b4d44d2f7" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "06dbae08-bc6b-4ca6-9162-3213784b9971",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Escape to Xoi Farmstay, nestled in the lush green valley of Lam Thuong in Northern Vietnam, just 250km from Hanoi and close to Ha Giang and Sapa. This is a haven for nature lovers, offering stunning rice fields, exotic mountains, springs, and waterfalls. Experience authentic local culture and delicious food in a truly non-touristy setting.", "Check-in before 1:00 AM, Checkout before 11:00 AM, 1 guest maximum", "Carbon monoxide alarm, No Smoke alarm", "Xoi Farmstay: Authentic Valley Retreat near Hanoi!" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "0bb50f31-e322-4b76-97dd-6a7fcf585d33",
                columns: new[] { "Description", "HouseRules", "Title" },
                values: new object[] { "Indulge in an unforgettable escape at this beachfront oasis, the Delta Hotels by Marriott Virginia Beach Waterfront. Perched on the stunning shores of Chesapeake Bay, this distinctive hotel offers panoramic water views and a private beach. Savor fresh oysters, fish, and coastal cuisine at our restaurant, all while enjoying inspiring bay vistas.", "Check-in: 4:00 PM - 12:00 AM, Checkout before 11:00 AM, 4 guests maximum", "Beachfront Oasis: Chesapeake Bay Views & Private Beach!" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "1adca40b-b8ff-4cea-b6e4-8e5f40d29c08",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Experience comfort and style in this room featuring a queen bed, ensuite bathroom, and air conditioning. Enjoy an excellent location, nestled between the vibrant Palermo and Recoleta neighborhoods. Just one block from Santa Fe Ave and two blocks from subway line D, putting Buenos Aires at your fingertips.", "Check-in before 4:00 AM, Checkout before 9:00 AM, 2 guests maximum", "No carbon monoxide alarm, No Smoke alarm", "Palermo/Recoleta Chic: Stylish Room with Ensuite & AC!" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "294e2751-203b-4beb-b21e-0bb96f082d7c",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Discover charming industrial character and premium comfort at The Foundry, ideally located near the vibrant shopping, dining, and nightlife of Admiralty Way, Lekki Phase 1. Relax by the swimming pool or enjoy endless entertainment with satellite TV, Netflix, and Amazon. Benefit from superfast fiber-optic Wi-Fi and uninterrupted 24/7 generator power back-up.", "Check-in before 2:00 AM, Checkout before 9:00 AM, 3 guests maximum", "Carbon monoxide alarm, Smoke alarm", "The Foundry: Luxe 2BR, Pool & Lekki Phase 1 Prime Location!" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "2ab6e4d1-79b9-4dba-9109-22ef75a29ff1",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Discover a truly unique stay at this guitar-shaped country house in Icheon, a renowned ceramic art village. This private retreat, featuring a spacious terrace on the 3rd floor of the Sera Guitar Culture Center, blends seamlessly with nature. Perfect for emotional healing and a memorable escape near Seoul.", "Check-in: 3:00 PM - 12:00 AM, Checkout before 11:00 AM, 2 guests maximum", "Carbon monoxide alarm not reported, Smoke alarm, Must climb stairs", "Unique Guitar House: Emotional Healing in Icheon-si, Korea!" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "2e3ed231-a2a6-4961-a1ba-f232d56c6f35",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Feel special from arrival to departure at Inone Mucho Selection Hotel, a beachfront haven with a private beach in one of Asarlik's clearest bays. Just a 5-minute drive from Bodrum center and a 5-minute walk from Gumbet bar street. Sip cocktails at our Iconic Beach restaurant, accompanied by events and DJ performances, for an unforgettable holiday.", "Check-in before 1:00 PM, Checkout before 10:00 PM, 2 guests maximum", "Carbon monoxide alarm, Smoke alarm", "Bodrum Beachfront Hotel: Private Beach & DJ Nights!" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "3c0e361a-51df-4e03-b8d0-2d7601aa60f6",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Discover a sunny, spacious, and clean room in the heart of Maadi, Cairo's upscale, green suburb. Nestled in a quiet area, this five-story building is just minutes from Road 9, offering an abundance of shops, cafes, and restaurants. Enjoy the perfect blend of tranquility and urban convenience, with downtown just a 15-minute ride away.", "Flexible check-in, 2 guests maximum, No pets", "No carbon monoxide alarm, No smoke alarm, Nearby lake, river, other body of water", "Sunny Maadi Room: Cairo Charm, Steps from Cafes!" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "3e7f99ab-228a-4d90-91c4-6adf8c12e048",
                columns: new[] { "CancellationPolicy", "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Free cancellation before May 17, Cancel before check-in on May 18 for a partial refund.", "Find serenity in this cozy 2-room apartment, a 10-12 minute walk from Al-Haram Al-Makkah. Listen to the call to prayer from your window, which offers a glimpse of Al-Haram Al-Sharif. Equipped with a surface kitchen (tea/coffee, mini-fridge, microwave, kettle), washing machine, and toiletries. Wheelchair accessible and free Wi-Fi. Located on the 17th floor of a high tower for a truly unique and pleasant stay.", "Check-in after 3:00 PM, Checkout before 12:00 PM, 7 guests maximum", "Carbon monoxide alarm, Smoke alarm installed", "Peaceful Mecca Retreat: Haram Views, 10-Min Walk!" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "4b04a76a-1608-4a8f-b09c-8d9043b83e16",
                columns: new[] { "CancellationPolicy", "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Free cancellation for 48 hours, Cancel before Jan 13 for a partial refund.", "Step back in time at Moinho das Feteiras, a beautifully restored 19th-century mill house with a 360-degree sea and surrounding view from the top floor. This charming retreat features a cozy bedroom, a well-decorated living room with a kitchenette, and a WC. Enjoy modern comforts with free WiFi, air conditioning, LED TV, and DVD player. Private parking offers extra security. Perfect for an unforgettable honeymoon!", "Check-in: 3:00 PM - 5:00 PM, Checkout before 10:00 AM, 2 guests maximum", "Climbing or play structure, Carbon monoxide alarm, Smoke alarm", "The Mill House: Romantic 19th-Century Sea View Retreat!" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "4e3d342-8e8d-4f1d-8123-2d09cb92b6a2",
                columns: new[] { "CancellationPolicy", "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Free cancellation before Oct 22, Cancel before check-in on Oct 23 for a partial refund.", "Discover this romantic loft featuring a mezzanine and a large, 180-degree oceanfront balcony. Enjoy a double bed, single bed, TV, Wi-Fi, and fan, all within a modernly decorated space. The equipped kitchen and private bathroom offer total comfort. Located on the fourth floor (no elevator) in a noble quarter, 5 minutes from the carnival circuit, between Surf and Paciencia beaches. Experience the most beautiful sunsets in Salvador with total security.", "3 guests maximum, Pets allowed", "Carbon monoxide alarm not reported, Smoke alarm not reported, Exterior security cameras on property", "Charming Oceanfront Loft: Salvador Sunset Views!" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "52a8df7d-c0b2-4ee3-8369-9daed4885f9f",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Unwind in a serene and fresh area, just a 3-minute drive from Ubud center. Our villa is nestled amidst lush rice fields, offering a truly authentic experience. Your friendly host is available 24/7 to ensure a delightful stay. Book for 3 nights and receive a complimentary 60-minute traditional Balinese massage for one person, perfect for completing your lazy days!", "Check-in before 3:00 PM, Checkout before 12:00 PM, 3 guests maximum", "Carbon monoxide alarm, No Smoke alarm, Nearby lake, river, other body of water", "Quiet Ubud Villa: Rice Fields & Balinese Massage!" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa",
                columns: new[] { "CancellationPolicy", "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Free cancellation before May 17, Cancel before check-in on May 18 for a partial refund.", "Featured on Dwell Magazine's cover, the Hawkeye Dome offers an extraordinary off-grid experience on 100 sprawling acres. Immerse yourself in nature with an updated 40-foot pool and hot tub that are truly spectacular. This unique, fully remodeled geodesic dome blends modern design with ultimate comfort. Endless hiking and complete privacy await. You'll never want to leave!", "Check-in after 3:00 PM, Checkout before 12:00 PM, 7 guests maximum", "Carbon monoxide alarm, Smoke alarm installed", "Hawkeye Dome: Epic Glamping with New Pool & Spa!" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "763e6c5f-1ad1-4071-b0e6-55e924624198",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Experience a warm welcome at Dar Ouassaggou, a comfortable guesthouse retreat in the stunning Atlas Mountains. Owner Houssine, a fluent English speaker, looks forward to hosting you. This small, inviting guesthouse features 13 en-suite rooms, each with a balcony, offering a perfect escape into nature and local hospitality.", "Check-in before 11:00 AM, Checkout before 12:00 AM, 3 guests maximum", "No carbon monoxide alarm, Smoke alarm", "Atlas Mountains Riad: Oussagou Guest House Retreat!" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4",
                columns: new[] { "CancellationPolicy", "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Free cancellation before May 17, Cancel before check-in on May 18 for a partial refund.", "Live like royalty in this elegant apartment within a famous Milanese castle! Located in vibrant Nolo, you're just steps from the M1 metro (10 mins to Duomo) and a 10-minute walk to Central Station. Excellent connections via trains, trams, and buses. Enjoy a neighborhood rich with restaurants, supermarkets, and nightlife. Featuring an 82\" Smart TV with Netflix/Prime, Wi-Fi, dishwasher, full kitchen, and coffee machine. Includes complete reception service for a comfortable stay.", "Check-in: 3:00 PM - 11:00PM, Checkout before 11:00 AM, 4 guests maximum", "Carbon monoxide alarm, Smoke alarm installed", "Milan Castle Apartment: Duomo 10 Mins, Central Hub!" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "a43ecbfa-7b0a-4f6b-9c88-987be3c4e3d3",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Stay in a prize-winning architectural building with this stunning modern Barcelona apartment. Impressive details abound, from ceiling-to-floor sloped windows to rich wood floors and designer textures. This space is cozy yet boasts a very hip, urban edge. Perfect for design enthusiasts and those seeking a modern Barcelona experience. High comfort and proximity to Sagrada Familia make it ideal for all guests.", "Check-in: 3:00 PM - 5:00 PM, Checkout before 10:00 AM, 2 guests maximum", "No carbon monoxide alarm, No smoke alarm, Heights without rails or protection", "Sunny Sagrada Familia Apartment: Modern Barcelona Gem!" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "a555515a-ff8a-4741-b0a4-db9be729198e",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Discover unparalleled luxury in this exquisite apartment in Gammarth's vibrant tourist area. Boasting breathtaking sea views and direct access to a private residents-only beach. The master suite features a private bathroom, with an additional second bathroom for convenience. Experience coastal living at its finest.", "Check-in after 3:00 PM, 4 guests maximum, Pets allowed", "Carbon monoxide alarm not reported, Smoke alarm not reported", "Luxury Gammarth Apartment: Sea Views & Private Beach Access!" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "c10d2d46-869a-46bc-a46d-90bdd958c252",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Step into a warm and cozy English cottage, tastefully adorned with antique furniture and surrounded by a lovely garden. Perfect for a relaxing countryside escape. Enjoy comfortable beds with blackout blinds for a peaceful night's sleep. Immerse yourself in the beauty of the serene surroundings.", "Check-in: (4:00 PM - 10:00 PM), Checkout before 11:00 AM, 4 guests maximum", "No carbon monoxide alarm, Nearby lake- river- other body of water, Smoke alarm", "Charming English Cottage: Antiques & Beautiful Garden!" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "c150e428-1c9a-43a2-be07-f4366875f1ce",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Discover this elegant and spacious penthouse on the 4th floor, freshly renovated in February 2025 and designed to comfortably sleep 6 guests. Featuring two double bedrooms, one single bedroom, and a sofa bed in the dining room, plus two bathrooms (one en-suite). Enjoy direct terrace access from every room, and easy Metro C access for exploring Rome!", "Check-in before 1:00 PM, Checkout before 10:00 PM, 2 guests maximum", "Carbon monoxide alarm, Smoke alarm", "Bright & New Rome Penthouse: Metro C Access, Sleeps 6!" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "c5c0d4db-b048-4ee4-8835-344900fd35b2",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Experience the tranquility of Heather Cottage, a charming small retreat on the edge of picturesque wetlands. Enjoy stunning views, a private gazebo with a covered firepit, and a dock overlooking a large pond. Located on our 5-acre free-range egg farm in Merville, BC, the pond is home to beavers, bald eagles, and blue herons. Explore a private walking trail and easy access to the One Spot Trail.", "Check-in after 3:00 PM, Checkout before 11:00 AM, 2 guests maximum", "Exterior security cameras on property, Carbon monoxide alarm, Smoke alarm", "Heather Cottage: Wetland Views, Firepit & Farm Charm!" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "cc4e48ea-ca54-4d32-a448-3c2c9d14f936",
                columns: new[] { "CancellationPolicy", "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Free cancellation before May 28, Cancel before check-in on Jun 2 for a partial refund.", "Experience the magic of ancient Egypt from your private oasis! This contemporary oriental studio boasts breathtaking panoramic views of the Giza Pyramids and Sphinx, 100% real and as stunning as the pictures. Relax in your private jacuzzi with iconic vistas. Just a 10-minute walk to the Pyramids entrance. Explore our unique experiences to enhance your trip. We're dedicated to providing magical hospitality!", "Check-in after 2:00 PM, Checkout before 11:00 AM, 2 guests maximum", "No Carbon monoxide alarm, No Smoke alarm", "Pyramid View Oasis: Jacuzzi & 10-Min Walk to Giza!" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "d8eecb1f-5583-4d64-a7dc-5aef5e2c498f",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Escape to 'The View,' an interior designer's guesthouse offering style and tranquility. This unique bergerie, a converted old stone shepherd's house, is nestled in Europe's largest mimosa forest with stunning views of the Cotes d'Azur and lower Alps. Tastefully designed for comfort and luxury, it provides everything for an unforgettable, tranquil escape. Accommodates up to 4 adults, with a small mezzanine for children.", "Check-in: 3:00 PM - 5:00 PM, Checkout before 10:00 AM, 2 guests maximum", "No carbon monoxide alarm, No smoke alarm, Heights without rails or protection", "The View: Designer Guesthouse with Pool & Mountain Vistas!" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "ef3b2df2-e539-4cb9-8eb6-4eeb833e694c",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Unwind at Kai Cottage, a tranquil escape perfectly situated to offer stunning nature views. This private haven provides a spacious terrace and a serene atmosphere, ideal for relaxation and rejuvenation. Experience peace and quiet in a beautiful setting.", "Check-in: 3:00 PM - 12:00 AM, Checkout before 11:00 AM, 2 guests maximum", "Carbon monoxide alarm not reported, Smoke alarm, Must climb stairs", "Kai Cottage: Serene Getaway with Nature Views!" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "efd964ab-dceb-4b96-b113-665c5684a102",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Just two hours from Bogotá, live an unparalleled experience in Colombia's most spectacular treehouse, perched eight meters high. Wake to birdsong, fall asleep to a gentle stream, and enjoy a five-star suite with hot water, a mini-fridge, and breathtaking views, all nestled within the tree branches. A truly unique natural retreat.", "Check-in before 3:00 PM, Checkout before 12:00 PM, 3 guests maximum", "Carbon monoxide alarm, No Smoke alarm, Nearby lake, river, other body of water", "Colombia's Most Spectacular Treehouse: 5-Star Nature Escape!" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "f1cc1b4c-b674-4a1a-89ee-5f7b4d44d2f7",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Immerse yourself in authentic Bedouin life at our unique Wadi Rum Sunset Cave. Gather around the fire, enjoy traditional food, and hear ancestral stories under a sky full of stars. A truly special escape from city life, offering a quiet environment for relaxation and meditation. This simple, traditional cave, built into the red rocks, is waterproof and safe, providing you with the entire desert to yourself.", "Check-in: 3:00 PM - 5:00 PM, Checkout before 10:00 AM, 2 guests maximum", "No carbon monoxide alarm, No smoke alarm, Heights without rails or protection", "Wadi Rum Sunset Cave: Authentic Bedouin Stargazing!" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "f1e8be41-4fd5-47e4-8960-12d8f4afc273",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Welcome to our brand new, cozy one-bedroom flat in the heart of Dubai! Enjoy incredible views of the bustling Business Bay canal and the iconic Burj Khalifa. Perfectly situated for both leisure and business travelers seeking a prime location and stunning vistas.", "Check-in before 1:00 AM, Checkout before 11:00 AM, 1 guest maximum", "Carbon monoxide alarm, Smoke alarm", "Central Dubai Flat: Burj Khalifa Views & Business Bay!" });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 1,
                column: "Amount",
                value: 120.00m);

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 2,
                column: "Amount",
                value: 30.00m);

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 3,
                column: "Amount",
                value: 50.00m);

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 25.00m, "Service Fee", "cc4e48ea-ca54-4d32-a448-3c2c9d14f936" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Amount", "Name" },
                values: new object[] { 100.00m, "Cleaning Fee" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 40.00m, "Pet Fee", "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 20.00m, "Service Fee", "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 80.00m, "Cleaning Fee", "3e7f99ab-228a-4d90-91c4-6adf8c12e048" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 18.00m, "Service Fee", "3e7f99ab-228a-4d90-91c4-6adf8c12e048" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Amount", "PropertyId" },
                values: new object[] { 90.00m, "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 25.00m, "Extra Guest Fee", "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 20.00m, "Extra Guest Fee", "4e3d342-8e8d-4f1d-8123-2d09cb92b6a2" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 15.00m, "Service Fee", "4e3d342-8e8d-4f1d-8123-2d09cb92b6a2" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 35.00m, "Pet Fee", "a43ecbfa-7b0a-4f6b-9c88-987be3c4e3d3" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 70.00m, "Cleaning Fee", "a43ecbfa-7b0a-4f6b-9c88-987be3c4e3d3" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Amount", "PropertyId" },
                values: new object[] { 110.00m, "f1cc1b4c-b674-4a1a-89ee-5f7b4d44d2f7" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 22.00m, "Service Fee", "f1cc1b4c-b674-4a1a-89ee-5f7b4d44d2f7" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Amount", "PropertyId" },
                values: new object[] { 60.00m, "d8eecb1f-5583-4d64-a7dc-5aef5e2c498f" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 75.00m, "Cleaning Fee", "4b04a76a-1608-4a8f-b09c-8d9043b83e16" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 30.00m, "Pet Fee", "2ab6e4d1-79b9-4dba-9109-22ef75a29ff1" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Amount", "PropertyId" },
                values: new object[] { 85.00m, "ef3b2df2-e539-4cb9-8eb6-4eeb833e694c" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 18.00m, "Extra Guest Fee", "3c0e361a-51df-4e03-b8d0-2d7601aa60f6" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 65.00m, "Cleaning Fee", "c5c0d4db-b048-4ee4-8835-344900fd35b2" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 28.00m, "Pet Fee", "0bb50f31-e322-4b76-97dd-6a7fcf585d33" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Amount", "PropertyId" },
                values: new object[] { 95.00m, "a555515a-ff8a-4741-b0a4-db9be729198e" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 22.00m, "Extra Guest Fee", "c10d2d46-869a-46bc-a46d-90bdd958c252" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 55.00m, "Cleaning Fee", "1adca40b-b8ff-4cea-b6e4-8e5f40d29c08" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Amount", "PropertyId" },
                values: new object[] { 58.00m, "294e2751-203b-4beb-b21e-0bb96f082d7c" });

            migrationBuilder.InsertData(
                table: "PropertyFees",
                columns: new[] { "Id", "Amount", "Name", "PropertyId" },
                values: new object[,]
                {
                    { 29, 32.00m, "Pet Fee", "06dbae08-bc6b-4ca6-9162-3213784b9971" },
                    { 30, 19.00m, "Extra Guest Fee", "f1e8be41-4fd5-47e4-8960-12d8f4afc273" },
                    { 31, 50.00m, "Cleaning Fee", "763e6c5f-1ad1-4071-b0e6-55e924624198" },
                    { 32, 52.00m, "Cleaning Fee", "efd964ab-dceb-4b96-b113-665c5684a102" },
                    { 33, 27.00m, "Pet Fee", "52a8df7d-c0b2-4ee3-8369-9daed4885f9f" },
                    { 34, 21.00m, "Extra Guest Fee", "c150e428-1c9a-43a2-be07-f4366875f1ce" },
                    { 35, 54.00m, "Cleaning Fee", "2e3ed231-a2a6-4961-a1ba-f232d56c6f35" }
                });

            migrationBuilder.InsertData(
                table: "PropertyGuests",
                columns: new[] { "GuestTypeId", "PropertyId", "GuestCount" },
                values: new object[,]
                {
                    { 2, "2ab6e4d1-79b9-4dba-9109-22ef75a29ff1", 5 },
                    { 4, "3c0e361a-51df-4e03-b8d0-2d7601aa60f6", 4 },
                    { 3, "3e7f99ab-228a-4d90-91c4-6adf8c12e048", 3 },
                    { 4, "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa", 2 },
                    { 2, "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4", 4 },
                    { 2, "a43ecbfa-7b0a-4f6b-9c88-987be3c4e3d3", 2 },
                    { 4, "d8eecb1f-5583-4d64-a7dc-5aef5e2c498f", 1 },
                    { 3, "ef3b2df2-e539-4cb9-8eb6-4eeb833e694c", 2 },
                    { 3, "f1cc1b4c-b674-4a1a-89ee-5f7b4d44d2f7", 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "PropertyGuests",
                keyColumns: new[] { "GuestTypeId", "PropertyId" },
                keyValues: new object[] { 2, "2ab6e4d1-79b9-4dba-9109-22ef75a29ff1" });

            migrationBuilder.DeleteData(
                table: "PropertyGuests",
                keyColumns: new[] { "GuestTypeId", "PropertyId" },
                keyValues: new object[] { 4, "3c0e361a-51df-4e03-b8d0-2d7601aa60f6" });

            migrationBuilder.DeleteData(
                table: "PropertyGuests",
                keyColumns: new[] { "GuestTypeId", "PropertyId" },
                keyValues: new object[] { 3, "3e7f99ab-228a-4d90-91c4-6adf8c12e048" });

            migrationBuilder.DeleteData(
                table: "PropertyGuests",
                keyColumns: new[] { "GuestTypeId", "PropertyId" },
                keyValues: new object[] { 4, "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa" });

            migrationBuilder.DeleteData(
                table: "PropertyGuests",
                keyColumns: new[] { "GuestTypeId", "PropertyId" },
                keyValues: new object[] { 2, "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4" });

            migrationBuilder.DeleteData(
                table: "PropertyGuests",
                keyColumns: new[] { "GuestTypeId", "PropertyId" },
                keyValues: new object[] { 2, "a43ecbfa-7b0a-4f6b-9c88-987be3c4e3d3" });

            migrationBuilder.DeleteData(
                table: "PropertyGuests",
                keyColumns: new[] { "GuestTypeId", "PropertyId" },
                keyValues: new object[] { 4, "d8eecb1f-5583-4d64-a7dc-5aef5e2c498f" });

            migrationBuilder.DeleteData(
                table: "PropertyGuests",
                keyColumns: new[] { "GuestTypeId", "PropertyId" },
                keyValues: new object[] { 3, "ef3b2df2-e539-4cb9-8eb6-4eeb833e694c" });

            migrationBuilder.DeleteData(
                table: "PropertyGuests",
                keyColumns: new[] { "GuestTypeId", "PropertyId" },
                keyValues: new object[] { 3, "f1cc1b4c-b674-4a1a-89ee-5f7b4d44d2f7" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "06dbae08-bc6b-4ca6-9162-3213784b9971",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Xoi Farmstay is located in a green valley of Lam Thuong in the North of Vietnam, about 250km from Hanoi and near to Hagiang and Sapa.This is a place for those who love nature, watching rice fields, exotic mountains, spring and waterfall, authentic local culture, good food, especially non touristy", "Check-in brfore 1:00 Am , Checkout before 11:00 AM , 1 guests maximum", "carbon monoxide alarm  ,No Smoke alarm", "TXoi Farmstay- Homefarm in the valley of Lam Thuong" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "0bb50f31-e322-4b76-97dd-6a7fcf585d33",
                columns: new[] { "Description", "HouseRules", "Title" },
                values: new object[] { "With panoramic water views, Delta Hotels by Marriott Virginia Beach Waterfront is an oasis on the shores of the breathtaking Chesapeake Bay.Thrill your palate with fresh oysters, fish, and coastal cuisine at our distinctive hotel restaurant, featuring inspiring water views.", "Check-in: 4:00 PM - 12:00 AM , Checkout before 11:00 AM ,4 guests maximum", "Escape To Our Beachfront Oasis | Private Beach" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "1adca40b-b8ff-4cea-b6e4-8e5f40d29c08",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Comfortable room, queen bed, bathroom in suite, with air conditioning. Excelent location, among Palermo and Recoleta neighborhoods, one block away from Santa Fe av and 2 blocks away from subway line D.", "Check-in brfore 4:00 Am , Checkout before 9:00 AM , 2 guests maximum", "No carbon monoxide alarm  ,No Smoke alarm", "Palermo/Recoleta. Stylish room w/ensuite-bath & AC" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "294e2751-203b-4beb-b21e-0bb96f082d7c",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Charming industrial character and premium homely comfort in the most desirable location. A leisurely stroll away from the shopping, dining & nightlife of Admiralty Way, Lekki Phase 1.Relax in the swimming pool or enjoy movies on satellite, Netflix or Amazon. Superfast optic-fibre broadband wi-fi. Uninterrupted 24/7 generator power back-up.", "Check-in brfore 2:00 Am , Checkout before 9:00 AM , 3 guests maximum", "carbon monoxide alarm  , Smoke alarm", "The Foundry. Luxury 2BR w/pool" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "2ab6e4d1-79b9-4dba-9109-22ef75a29ff1",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "This is a guitar-shaped country house located in Icheon, a ceramic art village. It is a private house with a spacious terrace on the 3rd floor of the Sera Guitar Culture Center, famous for its unique appearance in the Icheon Ceramic Art Village, which blends in very well with nature.", "Check-in: 3:00 PM - 12:00 AM  , Checkout before 11:00  AM , 2 guests maximum", "Carbon monoxide alarm not reported , Smoke alarm , Must climb stairs", "Emotional healing accommodation in Icheon-si, near Seoul" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "2e3ed231-a2a6-4961-a1ba-f232d56c6f35",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "You will feel special from the beginning to the end of your holiday at Inone Mucho Selection Hotel, located on the seafront with a private beach in one of the clearest bays of Asarlik.Our facility which is located 5 minutes drive away from Bodrum center and 5 minutes from Gumbet bar street by walk. You can have a pleasant time while sipping your cocktail at our Iconic Beach restaurant, accompanied by various events and DJ performances.", "Check-in brfore 1:00 PM , Checkout before 10:00 PM , 2 guests maximum", "carbon monoxide alarm  ,  Smoke alarm", "Inone Mucho Selection Hotel Deluxe Room B&B" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "3c0e361a-51df-4e03-b8d0-2d7601aa60f6",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Maadi is an uptown , green suburb with villas and gardens. My building is a five storey building . It is in a quiet area but a few minutes-walk away from Rd 9 where there are shops, cafes and restaurants. Everything you need is right here yet in 15 mins u can be in center of town.", "Flexible check-in , 2 guests maximum , No pets", "No carbon monoxide alarm , No smoke alarm ,Nearby lake, river, other body of water", "sunny, spacious, clean room in maadi, cairo.." });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "3e7f99ab-228a-4d90-91c4-6adf8c12e048",
                columns: new[] { "CancellationPolicy", "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Free cancellation before May 17 , Cancel before check-in on May 18 for a partial refund.", "Relax with this listing Small 2-room 7-bed apartment near Alharam Al Makkah with a maximum of 10 to 12 minutes' walk away The ears and prayer are also heard inside the rooms and the window appears from the window of the Haram Al-Sharif .We offer a Surface kitchen with tea and coffee supplies, a mini fridge, a microwave, a water kettle and more A washing machine is available and we provide toiletries from towels, shampoo, lotion, soap, and more We provide a wheelchair ,wi-fi .This place is in a high tower where the apartment is located on the 17th floor Wish you a unique and pleasant stay", "Check-in after 3:00 PM , Checkout before 12:00 PM , 7 guests maximum", "Carbon monoxide alarm ,Smoke alarm installed", "Rent an apartment near Alhar Mecca" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "4b04a76a-1608-4a8f-b09c-8d9043b83e16",
                columns: new[] { "CancellationPolicy", "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Free cancellation for 48 hours , Cancel before Jan 13 for a partial refund.", "Built in the 19th century, with a 360 degrees view over the sea and surroundings on the top floor.It features a Bedroom, a very well-decorated living room with kitchenette, and a WC.Free WiFi, air conditioning, Led TV and DVD player.Private parking inside the premises, providing extra security.Perfect for an unforgettable honeymoon experience.", "Check-in: 3:00 PM - 5:00 PM ,Checkout before 10:00 AM ,2 guests maximum", "Climbing or play structure , Carbon monoxide alarmSmoke alarm", "Moinho das Feteiras | The Mill House" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "4e3d342-8e8d-4f1d-8123-2d09cb92b6a2",
                columns: new[] { "CancellationPolicy", "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Free cancellation before Oct 22 , Cancel before check-in on Oct 23 for a partial refund.", "Romantic Loft with mezzanine and large balcony in front of the sea, double bed and 1 single bed, tv, wi-fi, fan, cabinet modern decoration, 180 degree terrace to the sea, equipped kitchen, bathroom, total comfort and privacy, fourth floor without elevator, 5 minutes from the carnival circuit, Noble Quarter of the city. Between the Surf and Paciencia beaches. Total security. The most beautiful sunset in Salvador", "3 guests maximum , Pets allowed", "Carbon monoxide alarm not reported , Smoke alarm not reported , Exterior security cameras on property", "(4) charming oceanfront loft!" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "52a8df7d-c0b2-4ee3-8369-9daed4885f9f",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Chill in a quite and fresh area only 3 min drive to Ubud center.Our villa located in the middle of rice field , offered you great experience.Friendly owner will assist you 24 hours by call to make sure you can enjoy the stay .Stay for 3 nights and you will get Free Traditional Balinese massage for 1 person for 60 min to complete the lazy days", "Check-in brfore 3:00 PM , Checkout before 12:00 PM , 3 guests maximum", "carbon monoxide alarm  , No Smoke alarm , Nearby lake, river, other body of water", "Quite Get Away near by theCenter" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa",
                columns: new[] { "CancellationPolicy", "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Free cancellation before May 17 , Cancel before check-in on May 18 for a partial refund.", "Updated pool and spa! Sitting on 100 acres, Hawkeye House, featured on the cover of the May 2019 issue of Dwell Magazine, is an off grid Geodesic Dome. It has a 40 foot pool and hot tub that you will have to see to believe. This unique and modern home has been fully remodeled with an attention to both comfort and detail. Amazing hikes and privacy are abundant here. Most people never want to leave the property", "Check-in after 3:00 PM , Checkout before 12:00 PM , 7 guests maximum", "Carbon monoxide alarm ,Smoke alarm installed", "Hawkeye Dome - New Pool and Spa" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "763e6c5f-1ad1-4071-b0e6-55e924624198",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Dar Ouassaggou's owner, Houssine, is a fluent English speaker and looks forward to welcoming you to his friendly guesthouse retreat in the Atlas Mountains, A Warm Welcome Awaits you at Dar Ouassaggou.It is a small comfortable guest house with 13 en suite rooms and balcony .", "Check-in brfore 11:00 Am , Checkout before 12:00 AM , 3 guests maximum", "No carbon monoxide alarm  , Smoke alarm", "Atlas Mountains Riad Oussagou" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4",
                columns: new[] { "CancellationPolicy", "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Free cancellation before May 17 , Cancel before check-in on May 18 for a partial refund.", "Elegant apartment inside the famous castle in Nolo, a royal choice right in the center of Milan A few steps away is the metro (M1 red for the Duomo 10 min), 10 minutes' walk for the central station. The apartment is well connected by trains, trams and buses The area is well supplied with restaurants, supermarkets, bars, clubs, etc. Complete comfort:82 Smart TV, Netflix, prime, wifi, dishwasher, kitchen, coffee machine The stay is included with a complete reception service", "Check-in: 3:00 PM - 11:00PM ,Checkout before 11:00 AM ,4 guests maximum", "Carbon monoxide alarm ,Smoke alarm installed", "Milano Duomo center 10 min Flat inside a castle" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "a43ecbfa-7b0a-4f6b-9c88-987be3c4e3d3",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Set in an architectural prize-winning building, this modern Barcelona apartment beauty has impressive detail throughout. Ceiling-to-floor sloped windows, wood floor, and other soft designer textures accentuate this spectacular space. It is cozy and welcoming but with a very hip, urban edge.Design enthusiasts and those looking for that modern Barcelona feel will love the apartment. However, high-comfort and proximity to the Sagrada Familia suits all tastes.", "Check-in: 3:00 PM - 5:00 PM ,Checkout before 10:00 AM ,2 guests maximum", "No carbon monoxide alarm , No smoke alarm , Heights without rails or protectio", "Sunny and cozy Apartment Sagrada Familia" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "a555515a-ff8a-4741-b0a4-db9be729198e",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Discover this luxury apartment in Gammarth, in the tourist area, with sea views and direct access to a private beach reserved for residents. The master suite includes a private bathroom, and a second bathroom is available", "Check-in after 3:00 PM,4 guests maximum,Pets allowed", "Carbon monoxide alarm not reported , Smoke alarm not reported", "Sea View S2: Waterfront, Private Beach" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "c10d2d46-869a-46bc-a46d-90bdd958c252",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Warm and cosy cottage decorated with antique furniture, with a lovely garden. Perfect if you're looking for a relaxing stay in beautiful countryside. The bedroom windows have blackout blinds and the beds are very comfortable.", "Check-in: (4:00 PM - 10:00 PM) , Checkout before 11:00 AM , 4 guests maximum", "No carbon monoxide alarm , Nearby lake- river- other body of water , Smoke alarm", "Cosy English cottage with beautiful garden" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "c150e428-1c9a-43a2-be07-f4366875f1ce",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Elegant and spacious apartment on the 4th floor, designed and realized for 6 people.Totally renovated in February 2025.,Composed of 2 double bedrooms, 1 single bedroom and a sofa bed in the dining room.,2 bathrooms of which one inside the double room.It is possible to access the terrace from each room.", "Check-in brfore 1:00 PM , Checkout before 10:00 PM , 2 guests maximum", "carbon monoxide alarm  ,  Smoke alarm", "[*Bright new Metro C penthouse*]." });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "c5c0d4db-b048-4ee4-8835-344900fd35b2",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Charming small cottage situated on the edge of wetlands with beautiful views. Private gazebo with covered firepit and a dock over looking the large pond. Located on our 5 acre free range egg farm in Merville, BC. The pond is home to a family of beavers, bald eagles, blue heron and various birds. Private walking trail off the cottage and access to the One Spot Trail at the end of our private drive.", "Check-in after 3:00 PM,Checkout before 11:00 AM,2 guests maximum", "Exterior security cameras on property ,Carbon monoxide alarm , Smoke alarm", "Heather Cottage - Beautiful Wetland Views" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "cc4e48ea-ca54-4d32-a448-3c2c9d14f936",
                columns: new[] { "CancellationPolicy", "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Free cancellation before May 28 , Cancel before check-in on Jun 2 for a partial refund.", "Enjoy your stay with Panoramic View of the giza pyramids and sphinx .Yes! view and pictures are all 100% real. (Be sure to check out our other listings too) Indulge in a stunning view of all the Giza Pyramids from anywhere within this contemporary oriental studio or while relaxing in the Jacuzzi. It is also a 10 min walk from the Pyramids entrance gate. To make the most of your trip, make sure to check out our experiences!We're committed to providing our guests the magical hospitality", "Check-in after 2:00 PM , Checkout before 11:00 AM , 2 guests maximum", "No Carbon monoxide alarm , No Smoke alarm ", "Entire rental unit in Nazlet El-Semman, Egypt" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "d8eecb1f-5583-4d64-a7dc-5aef5e2c498f",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Interior designer's own guesthouse, this unique place has a style all its own. Escape the ordinary and immerse yourself in comfort, calm and luxury at our charming bergerie, a conversion from a shepherd's old stone house! Nestled in the heart of the largest mimosa forest in Europe, overlooking the Cotes d'Azur and lower Alps, our tastefully designed retreat offers everything you need for an unforgettable tranquillity.We welcome up to 4 adults and have a small mezzanine for children.", "Check-in: 3:00 PM - 5:00 PM ,Checkout before 10:00 AM ,2 guests maximum", "No carbon monoxide alarm , No smoke alarm , Heights without rails or protectio", "New! The View: See to Mouintain (with pool)" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "ef3b2df2-e539-4cb9-8eb6-4eeb833e694c",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "This is a guitar-shaped country house located in Icheon, a ceramic art village. It is a private house with a spacious terrace on the 3rd floor of the Sera Guitar Culture Center, famous for its unique appearance in the Icheon Ceramic Art Village, which blends in very well with nature.", "Check-in: 3:00 PM - 12:00 AM  , Checkout before 11:00  AM , 2 guests maximum", "Carbon monoxide alarm not reported , Smoke alarm , Must climb stairs", "Kai Cottage" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "efd964ab-dceb-4b96-b113-665c5684a102",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Two hours from Bogotá on the Bogotá-Sasaima road, live the unique experience of staying in a tree eight meters high.Wake up to the chirping of birds and fall asleep to the sound of the stream below.Enjoy a five-star suite with all the amenities in the branches of the trees.The cabin has hot water, a mini-fridge, and the most spectacular view.", "Check-in brfore 3:00 PM , Checkout before 12:00 PM , 3 guests maximum", "carbon monoxide alarm  , No Smoke alarm , Nearby lake, river, other body of water", "The most spectacular treehouse in Colombia." });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "f1cc1b4c-b674-4a1a-89ee-5f7b4d44d2f7",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "To give you the best experience of the authentic Bedouin life style, we will gather around the fire, cook our traditional food and tell you stories of our ancestors, while looking at the sky full of stars.Without a lie, this experience will be very special, if you used to cities and crowd in your everyday life.We created the space in a very simple, traditional and nomadic way. The Cave is inside the red rocks, waterproof and safe from all sides. Here you will have the whole Desert for yourself to get away from normal life, to relax, be in a quiet environment and meditate.", "Check-in: 3:00 PM - 5:00 PM ,Checkout before 10:00 AM ,2 guests maximum", "No carbon monoxide alarm , No smoke alarm , Heights without rails or protectio", "Wadi Rum Sunset Cave" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: "f1e8be41-4fd5-47e4-8960-12d8f4afc273",
                columns: new[] { "Description", "HouseRules", "SafteyInfo", "Title" },
                values: new object[] { "Welcome to our brand new one-bedroom flat offering incredible views of Business Bay canal and the iconic Burj Khalifa.", "Check-in brfore 1:00 Am , Checkout before 11:00 AM , 1 guests maximum", "carbon monoxide alarm  , Smoke alarm", "Cosy flat in the heart of Dubai" });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyAvailabilities",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 1,
                column: "Amount",
                value: 1212.09m);

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 2,
                column: "Amount",
                value: 442.09m);

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 3,
                column: "Amount",
                value: 600m);

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 1200m, "Cleaning Fee", "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Amount", "Name" },
                values: new object[] { 600m, "Pet Fee" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 950.50m, "Cleaning Fee", "3e7f99ab-228a-4d90-91c4-6adf8c12e048" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 900.12m, "Cleaning Fee", "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 330.00m, "Extra Guest Fee", "4e3d342-8e8d-4f1d-8123-2d09cb92b6a2" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 442.09m, "Pet Fee", "a43ecbfa-7b0a-4f6b-9c88-987be3c4e3d3" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Amount", "PropertyId" },
                values: new object[] { 800.75m, "f1cc1b4c-b674-4a1a-89ee-5f7b4d44d2f7" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 113.09m, "Cleaning Fee", "d8eecb1f-5583-4d64-a7dc-5aef5e2c498f" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 510.00m, "Cleaning Fee", "4b04a76a-1608-4a8f-b09c-8d9043b83e16" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 250.00m, "Pet Fee", "2ab6e4d1-79b9-4dba-9109-22ef75a29ff1" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 789.99m, "Cleaning Fee", "ef3b2df2-e539-4cb9-8eb6-4eeb833e694c" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 199.99m, "Extra Guest Fee", "3c0e361a-51df-4e03-b8d0-2d7601aa60f6" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Amount", "PropertyId" },
                values: new object[] { 450.00m, "c5c0d4db-b048-4ee4-8835-344900fd35b2" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 320.00m, "Pet Fee", "0bb50f31-e322-4b76-97dd-6a7fcf585d33" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Amount", "PropertyId" },
                values: new object[] { 670.00m, "a555515a-ff8a-4741-b0a4-db9be729198e" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 275.50m, "Extra Guest Fee", "c10d2d46-869a-46bc-a46d-90bdd958c252" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 390.00m, "Cleaning Fee", "1adca40b-b8ff-4cea-b6e4-8e5f40d29c08" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Amount", "PropertyId" },
                values: new object[] { 425.99m, "294e2751-203b-4beb-b21e-0bb96f082d7c" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 515.49m, "Pet Fee", "06dbae08-bc6b-4ca6-9162-3213784b9971" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 398.89m, "Extra Guest Fee", "f1e8be41-4fd5-47e4-8960-12d8f4afc273" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 300.00m, "Cleaning Fee", "763e6c5f-1ad1-4071-b0e6-55e924624198" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Amount", "PropertyId" },
                values: new object[] { 345.00m, "efd964ab-dceb-4b96-b113-665c5684a102" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 410.00m, "Pet Fee", "52a8df7d-c0b2-4ee3-8369-9daed4885f9f" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "Amount", "Name", "PropertyId" },
                values: new object[] { 289.00m, "Extra Guest Fee", "c150e428-1c9a-43a2-be07-f4366875f1ce" });

            migrationBuilder.UpdateData(
                table: "PropertyFees",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Amount", "PropertyId" },
                values: new object[] { 378.00m, "2e3ed231-a2a6-4961-a1ba-f232d56c6f35" });

            migrationBuilder.InsertData(
                table: "PropertyGuests",
                columns: new[] { "GuestTypeId", "PropertyId", "GuestCount" },
                values: new object[,]
                {
                    { 1, "2ab6e4d1-79b9-4dba-9109-22ef75a29ff1", 5 },
                    { 1, "3c0e361a-51df-4e03-b8d0-2d7601aa60f6", 4 },
                    { 1, "3e7f99ab-228a-4d90-91c4-6adf8c12e048", 3 },
                    { 1, "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa", 2 },
                    { 1, "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4", 4 },
                    { 1, "a43ecbfa-7b0a-4f6b-9c88-987be3c4e3d3", 2 },
                    { 1, "d8eecb1f-5583-4d64-a7dc-5aef5e2c498f", 1 },
                    { 1, "ef3b2df2-e539-4cb9-8eb6-4eeb833e694c", 2 },
                    { 1, "f1cc1b4c-b674-4a1a-89ee-5f7b4d44d2f7", 4 }
                });
        }
    }
}
