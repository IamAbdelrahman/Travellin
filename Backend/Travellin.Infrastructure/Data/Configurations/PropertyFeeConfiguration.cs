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
    public class PropertyFeeConfiguration : IEntityTypeConfiguration<PropertyFee>
    {
        public void Configure(EntityTypeBuilder<PropertyFee> builder)
        {
            builder.ToTable("PropertyFees");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();


            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Amount)
                .IsRequired()
                .HasColumnType("decimal(16, 2)");

            builder.HasOne(x => x.Property)
            .WithMany(p => p.PropertyFees)
            .HasForeignKey(x => x.PropertyId);

            builder.HasData(PropertyFeeSeed.Data);
        }
    }
}
