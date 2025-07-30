using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travellin.Core.Entities;
using Travellin.Travellin.Core.Enums;

namespace Travellin.Infrastructure.Data.Configurations
{
    class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(x => x.Id);

            // ID configuration
            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWID()");

            // Property relationships
            builder.Property(x => x.BookingId)
                .IsRequired();

            builder.Property(x => x.StripeSessionId)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.HostStripeAccountId)
                .IsRequired(false)
                .HasMaxLength(255);

            builder.Property(x => x.StripePaymentIntentId)
                .HasMaxLength(255)
                .IsRequired(false);

            // Amount configuration
            builder.Property(x => x.Amount)
                .IsRequired()
                .HasColumnType("decimal(16,2)");

            // Currency configuration
            builder.Property(x => x.Currency)
                .IsRequired()
                .HasMaxLength(10)
                .HasDefaultValue("usd");

            // Status configuration with string conversion
            builder.Property(x => x.Status)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (PaymentStatus)Enum.Parse(typeof(PaymentStatus), v))
                .HasDefaultValue(PaymentStatus.Pending);

            // Timestamp configurations
            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();

            // Relationships
            builder.HasOne(x => x.Booking)
                .WithMany(x => x.Payments)
                .HasForeignKey(x => x.BookingId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes for better query performance
            builder.HasIndex(x => x.BookingId);
            builder.HasIndex(x => x.StripeSessionId)
                .IsUnique();
            builder.HasIndex(x => x.StripePaymentIntentId);
            builder.HasIndex(x => x.Status);
        }
    }
}
