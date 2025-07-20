using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travellin.Core.Entities;
using Travellin.Infrastructure.Data.Seeds;

namespace Travellin.Infrastructure.Data.Configurations
{
    public class FavoritePropertyConfiguration : IEntityTypeConfiguration<FavoriteProperty>
    {
        public void Configure(EntityTypeBuilder<FavoriteProperty> builder)
        {
            builder.HasKey(x => new { x.PropertyId, x.UserId });
            builder.Property(x => x.PropertyId)
                .IsRequired();
            builder.Property(x => x.UserId)
                .IsRequired();

            builder.HasOne(x => x.Property)
                .WithMany()
                .HasForeignKey(x => x.PropertyId)
            .OnDelete(DeleteBehavior.Restrict);



            builder.HasOne(x => x.User)
               .WithMany()
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Restrict);


            builder.HasData(FavoritePropertySeed.Data);

        }
    }
}
