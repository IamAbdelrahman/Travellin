using Microsoft.EntityFrameworkCore;
using System;
using Travellin.Travellin.Core.Entities;

namespace Travellin.Travellin.Infrastructure.Data
{
    public class AirbnbDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<HostVerification> HostVerifications { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<AmenityCategory> AmenityCategories { get; set; }
        public DbSet<PropertyAmenity> PropertyAmenities { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<PropertyAvailability> PropertyAvailabilities { get; set; }
        public DbSet<PropertyFee> PropertyFees { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingGuest> BookingGuests { get; set; }
        public DbSet<CancellationPolicy> CancellationPolicies { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<UserUsedPromotion> UserUsedPromotions { get; set; }
        public DbSet<Violation> Violations { get; set; }

        public AirbnbDbContext(DbContextOptions<AirbnbDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure unique constraints
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Configure many-to-many: Property-Amenity
            modelBuilder.Entity<PropertyAmenity>()
                .HasKey(pa => new { pa.PropertyId, pa.AmenityId });

            modelBuilder.Entity<PropertyAmenity>()
                .HasOne(pa => pa.Property)
                .WithMany(p => p.Amenities)
                .HasForeignKey(pa => pa.PropertyId);

            modelBuilder.Entity<PropertyAmenity>()
                .HasOne(pa => pa.Amenity)
                .WithMany(a => a.Properties)
                .HasForeignKey(pa => pa.AmenityId);

            // Configure many-to-many: User-Favourite-Property
            modelBuilder.Entity<Favourite>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favourites)
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<Favourite>()
                .HasOne(f => f.Property)
                .WithMany(p => p.Favourites)
                .HasForeignKey(f => f.PropertyId);

            // Configure Violation relationships
            modelBuilder.Entity<Violation>()
                .HasOne(v => v.ReportedBy)
                .WithMany(u => u.ViolationsReported)
                .HasForeignKey(v => v.ReportedById);

            modelBuilder.Entity<Violation>()
                .HasOne(v => v.ReportedProperty)
                .WithMany(p => p.Violations)
                .HasForeignKey(v => v.ReportedPropertyId)
                .IsRequired(false);

            modelBuilder.Entity<Violation>()
                .HasOne(v => v.ReportedUser)
                .WithMany(u => u.ViolationsAgainst)
                .HasForeignKey(v => v.ReportedUserId)
                .IsRequired(false);

            // Configure Message relationships
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Conversation)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ConversationId);

            // Configure CoHostAssignment relationships
            modelBuilder.Entity<CoHostAssignment>()
                .HasOne(cha => cha.Host)
                .WithMany(u => u.HostAssignments)
                .HasForeignKey(cha => cha.HostId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CoHostAssignment>()
                .HasOne(cha => cha.CoHost)
                .WithMany(u => u.CoHostAssignments)
                .HasForeignKey(cha => cha.CoHostId)
                .OnDelete(DeleteBehavior.Restrict);

            // Add indexes for performance
            modelBuilder.Entity<Property>()
                .HasIndex(p => new { p.Latitude, p.Longitude });

            modelBuilder.Entity<Booking>()
                .HasIndex(b => new { b.PropertyId, b.StartDate, b.EndDate });

            modelBuilder.Entity<Violation>()
                .HasIndex(v => v.Status);
        }
    }
}
