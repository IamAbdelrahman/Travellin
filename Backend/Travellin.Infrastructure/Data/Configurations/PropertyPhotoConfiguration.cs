using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travellin.Core.Entities;
using Travellin.Infrastructure.Data.Seeds;

namespace Travellin.Infrastructure.Data.Configurations
{
    class PropertyPhotoConfiguration : IEntityTypeConfiguration<PropertyPhoto>
    {
        public void Configure(EntityTypeBuilder<PropertyPhoto> builder)
        {
            builder.HasKey(x => x.PhotoId);

            builder.Property(x => x.PhotoId)
                .IsRequired();

            builder.Property(x => x.PropertyId)
                .IsRequired();

            //TIP: DateTime.UtcNow.ToString("O") // "O" = ISO 8601 with ms
            builder.Property(x => x.TouchedAt)
                .IsRequired()
                .HasColumnType("datetime2(3)");

            builder.HasOne(x => x.Property)
                .WithMany(x => x.PropertyPhotos)
                .HasForeignKey(x => x.PropertyId);

            builder.HasOne(x => x.FileUpload)
                .WithOne(x => x.PropertyPhoto)
                .HasForeignKey<PropertyPhoto>(x => x.PhotoId);


            builder.HasData(PropertyPhotoSeed.Data);
        }
    }
}
