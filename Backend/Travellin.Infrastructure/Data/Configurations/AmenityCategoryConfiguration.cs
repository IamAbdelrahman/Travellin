using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travellin.Core.Entities;
using Travellin.Infrastructure.Data.Seeds;

namespace Travellin.Infrastructure.Data.Configurations
{
    class AmenityCategoryConfiguration : IEntityTypeConfiguration<AmenityCategory>
    {
        public void Configure(EntityTypeBuilder<AmenityCategory> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasData(AmenityCategorySeed.Data);
        }
    }
}
