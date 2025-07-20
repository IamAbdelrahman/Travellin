using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Travellin.Core.Entities;
using System.Reflection;


namespace Travellin.Infrastructure.Data
{
    public class TravellinDbContext : IdentityDbContext<AppUser>
    {
        public TravellinDbContext(DbContextOptions<TravellinDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<GuestType> GuestTypes { get; set; }
        public virtual DbSet<AmenityCategory> AmenityCategories { get; set; }
        public virtual DbSet<Amenity> Amenities { get; set; }
        public virtual DbSet<PropertyType> PropertyTypes { get; set; }
        public virtual DbSet<PropertySpaceType> PropertySpaceTypes { get; set; }
        public virtual DbSet<PropertySpaceItemType> PropertySpaceItemTypes { get; set; }
        public virtual DbSet<FileUpload> FileUploads { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<PropertyPhoto> PropertyPhotos { get; set; }
        public virtual DbSet<PropertyGuest> PropertyGuests { get; set; }
        public virtual DbSet<PropertyFee> PropertyFees { get; set; }
        public virtual DbSet<PropertyAmenity> PropertyAmenities { get; set; }
        public virtual DbSet<PropertyAvailability> PropertyAvailabilities { get; set; }
        public virtual DbSet<PropertySpace> PropertySpaces { get; set; }
        public virtual DbSet<PropertySpaceItem> PropertySpaceItems { get; set; }
        public virtual DbSet<FavoriteProperty> FavoriteProperties { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<BookingGuest> BookingGuests { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<HostUpgradeRequest> HostUpgradeRequests { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Violation> Violations { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Conversation> Conversations { get; set; }
    }
}