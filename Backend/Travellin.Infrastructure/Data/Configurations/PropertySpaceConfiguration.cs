using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travellin.Core.Entities;
using Travellin.Infrastructure.Data.Seeds;

namespace Travellin.Infrastructure.Data.Configurations
{
    class PropertySpaceConfiguration : IEntityTypeConfiguration<PropertySpace>
    {
        public void Configure(EntityTypeBuilder<PropertySpace> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.PropertySpaceTypeId)
                .IsRequired();

            builder.Property(x => x.PropertyId)
                .IsRequired();

            builder.Property(x => x.IsShared)
                .IsRequired()
                .HasDefaultValue(false);

            builder.HasOne(x => x.PropertySpaceType)
                .WithMany(x => x.PropertySpaces)
                .HasForeignKey(x => x.PropertySpaceTypeId);

            builder.HasOne(x => x.Property)
                .WithMany(x => x.PropertySpaces)
                .HasForeignKey(x => x.PropertyId);

            builder.HasData(PropertySpaceSeed.Data);
        }
    }
}
