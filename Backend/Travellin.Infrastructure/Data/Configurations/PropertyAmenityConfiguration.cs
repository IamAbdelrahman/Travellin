using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travellin.Core.Entities;
using Travellin.Infrastructure.Data.Seeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travellin.Infrastructure.Data.Configurations
{
    public class PropertyAmenityConfiguration : IEntityTypeConfiguration<PropertyAmenity>
    {
        public void Configure(EntityTypeBuilder<PropertyAmenity> builder)
        {
            builder.HasKey(x => new { x.PropertyId, x.AmenityId });
            builder.Property(x => x.PropertyId)
               .IsRequired();

            builder.Property(x => x.AmenityId)
                .IsRequired();

            builder.HasOne(x => x.Property)
                .WithMany()
                .HasForeignKey(x => x.PropertyId);

            builder.HasOne(x => x.Amenity)
                .WithMany()
                .HasForeignKey(x => x.AmenityId);

            builder.HasData(PropertyAmenitySeed.Data);
        }
    }
}
