using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travellin.Core.Entities;
using Travellin.Infrastructure.Data.Seeds;

namespace Travellin.Infrastructure.Data.Configurations
{
    class PropertySpaceItemConfiguration : IEntityTypeConfiguration<PropertySpaceItem>
    {
        public void Configure(EntityTypeBuilder<PropertySpaceItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired();

            builder.Property(x => x.PropertySpaceItemTypeId)
                .IsRequired(false);

            builder.Property(x => x.PropertySpaceId)
                .IsRequired();

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.HasOne(x => x.PropertySpaceItemType)
                .WithMany(x => x.PropertySpaceItems)
                .HasForeignKey(x => x.PropertySpaceItemTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.PropertySpace)
                .WithMany(x => x.PropertySpaceItems)
                .HasForeignKey(x => x.PropertySpaceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(PropertySpaceItemSeed.Data);
        }
    }
}
