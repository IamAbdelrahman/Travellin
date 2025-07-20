using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travellin.Core.Entities;
using Travellin.Infrastructure.Data.Seeds;

namespace Travellin.Infrastructure.Data.Configurations
{
    class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.RegionId)
                .IsRequired();

            builder.HasOne(x => x.Region)
                .WithMany(x => x.Countries)
                .HasForeignKey(x => x.RegionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(CountrySeed.Data);
        }
    }
}
