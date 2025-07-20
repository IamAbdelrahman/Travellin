using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travellin.Core.Entities;
using Travellin.Infrastructure.Data.Seeds;

namespace Travellin.Infrastructure.Data.Configurations
{
    class BookingGuestConfiguration : IEntityTypeConfiguration<BookingGuest>
    {
        public void Configure(EntityTypeBuilder<BookingGuest> builder)
        {
            builder.HasKey(x => new { x.BookingId, x.GuestTypeId });

            builder.Property(x => x.BookingId)
                .IsRequired();

            builder.Property(x => x.GuestTypeId)
                .IsRequired();


            builder.Property(x => x.GuestCount)
                .IsRequired()
                .HasDefaultValue(1);


            builder.HasOne(x => x.Booking)
                .WithMany(b => b.BookingGuests)
                .HasForeignKey(x => x.BookingId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(x => x.GuestType)
                .WithMany()
                .HasForeignKey(x => x.GuestTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Index configuration
            builder.HasIndex(x => x.BookingId);

            // Seed data
            builder.HasData(BookingGuestSeed.Data);
        }
    }
}
