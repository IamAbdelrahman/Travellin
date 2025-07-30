using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travellin.Core.Entities;
using Travellin.Infrastructure.Data.Seeds;

namespace Travellin.Infrastructure.Data.Configurations
{
    class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(x => x.UserId);

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.FirstName)
                .IsRequired(false)
                .HasMaxLength(100);

            builder.Property(x => x.LastName)
                .IsRequired(false)
                .HasMaxLength(100);

            builder.Property(x => x.Bio)
                .IsRequired(false);

            builder.Property(x => x.CountryId)
                .IsRequired(false);

            builder.Property(x => x.StripeAccountId)
                .IsRequired(false)
                .HasMaxLength(100);

            builder.Property(x => x.BirthDate)
                .IsRequired(false);

            builder.Property(x => x.PhotoId)
                .IsRequired(false);


            builder.HasOne(x => x.AppUser)
                .WithOne(x => x.UserProfile)
                .HasForeignKey<UserProfile>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Photo)
                .WithOne()
                .HasForeignKey<UserProfile>(x => x.PhotoId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.Photo)
                .WithOne()
                .HasForeignKey<UserProfile>(x => x.PhotoId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasData(UserProfileSeed.Data);
        }
    }
}
