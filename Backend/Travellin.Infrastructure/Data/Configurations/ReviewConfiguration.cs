using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travellin.Core.Entities;
using Travellin.Infrastructure.Data.Seeds;

namespace Travellin.Infrastructure.Data.Configurations
{
    class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.BookingId)
                .IsRequired();

            builder.Property(x => x.Comment)
                 .IsRequired();

            builder.Property(r => r.Cleanliness)
    .IsRequired()
    .HasColumnType("decimal(2,1)");

            builder.Property(x => x.Accuracy)
                .IsRequired()
                .HasColumnType("decimal(2,1)");

            builder.Property(x => x.CheckIn)
                .IsRequired()
                .HasColumnType("decimal(2,1)");

            builder.Property(x => x.Communication)
                .IsRequired()
                .HasColumnType("decimal(2,1)");

            builder.Property(x => x.Location)
                .IsRequired()
                .HasColumnType("decimal(2,1)");

            builder.Property(x => x.Value)
                .IsRequired()
                .HasColumnType("decimal(2,1)");

            // Timestamps
            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();


            builder.HasOne(x => x.Booking)
                .WithOne(x => x.Review)
                .HasForeignKey<Review>(x => x.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.BookingId);
            builder.HasIndex(x => x.CreatedAt);

            builder.HasData(ReviewSeed.Data);
        }
    }
}
