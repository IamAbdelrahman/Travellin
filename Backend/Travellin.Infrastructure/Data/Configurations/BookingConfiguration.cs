using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travellin.Core.Entities;
using Travellin.Infrastructure.Data.Seeds;
using Travellin.Travellin.Core.Enums;

namespace Travellin.Infrastructure.Data.Configurations
{
    class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(x => x.Id);

            // ID configuration
            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWID()");

            // Property relationships
            builder.Property(x => x.PropertyId)
                .IsRequired();

            builder.Property(x => x.UserId)
                .IsRequired();

            // Date configurations
            builder.Property(x => x.CheckIn)
                .IsRequired();

            builder.Property(x => x.CheckOut)
                .IsRequired();

            // Price configurations
            builder.Property(x => x.PricePerNight)
                .IsRequired()
                .HasColumnType("decimal(16,2)");

            builder.Property(x => x.TotalFees)
                .IsRequired()
                .HasColumnType("decimal(16,2)");

            // Status configuration with string conversion
            builder.Property(x => x.Status)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (BookingStatus)Enum.Parse(typeof(BookingStatus), v));

            // Timestamp configurations
            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();


            builder.Ignore(x => x.TotalAmount);

            // Relationships
            builder.HasOne(x => x.Property)
                .WithMany(x => x.Bookings)
                .HasForeignKey(x => x.PropertyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes for better query performance
            builder.HasIndex(x => x.PropertyId);
            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.CheckIn);
            builder.HasIndex(x => x.CheckOut);
            builder.HasIndex(x => x.Status);

            // Seed data
            builder.HasData(BookingSeed.Data);
        }
    }
}