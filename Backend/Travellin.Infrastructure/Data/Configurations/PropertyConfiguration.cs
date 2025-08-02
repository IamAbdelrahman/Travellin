using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travellin.Infrastructure.Data.Seeds;

namespace Travellin.Infrastructure.Data.Configurations
{
    class PropertyConfiguration : IEntityTypeConfiguration<Core.Entities.Property>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.Property> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Description)
                .IsRequired(false);

            builder.Property(x => x.OwnerId)
                .IsRequired();

            builder.Property(x => x.PropertyTypeId)
                .IsRequired();

            builder.Property(x => x.LocationId)
                .IsRequired();

            builder.Property(x => x.PricePerNight)
                .IsRequired(true)
                .HasColumnType("decimal(16,2)");

            builder.Property(x => x.Latitude)
                .IsRequired()
                .HasColumnType("decimal(9,6)");

            builder.Property(x => x.Longitude)
                .IsRequired()
                .HasColumnType("decimal(9,6)");

            builder.Property(x => x.SafteyInfo)
                .IsRequired(false);

            builder.Property(x => x.HouseRules)
                .IsRequired(false);

            builder.Property(x => x.CancellationPolicy)
                .IsRequired(false);

            builder.Property(x => x.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(x => x.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(x => x.IsInstantBook).IsRequired().HasDefaultValue(false);
            builder.HasOne(x => x.Owner)
                .WithMany(x => x.Properties)
                .HasForeignKey(x => x.OwnerId);

            builder.HasOne(x => x.Location)
                .WithMany(x => x.Properties)
                .HasForeignKey(x => x.LocationId);

            builder.HasOne(x => x.PropertyType)
                .WithMany(x => x.Properties)
                .HasForeignKey(x => x.PropertyTypeId);


            builder.HasData(PropertySeed.Data);
        }
    }
}
