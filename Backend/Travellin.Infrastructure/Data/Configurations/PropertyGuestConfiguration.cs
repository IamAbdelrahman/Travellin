using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travellin.Core.Entities;
using Travellin.Infrastructure.Data.Seeds;

namespace Travellin.Infrastructure.Data.Configurations
{
    class PropertyGuestConfiguration : IEntityTypeConfiguration<PropertyGuest>
    {
        public void Configure(EntityTypeBuilder<PropertyGuest> builder)
        {
            builder.HasKey(x => new { x.PropertyId, x.GuestTypeId });

            builder.Property(x => x.PropertyId)
                .IsRequired();

            builder.Property(x => x.GuestTypeId)
                .IsRequired();

            builder.Property(x => x.GuestCount)
                .IsRequired();

            builder.HasOne(x => x.Property)
                .WithMany(x => x.PropertyGuests)
                .HasForeignKey(x => x.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.GuestType)
                .WithMany(x => x.PropertyGuests)
                .HasForeignKey(x => x.GuestTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(PropertyGuestSeed.Data);
        }
    }
}
