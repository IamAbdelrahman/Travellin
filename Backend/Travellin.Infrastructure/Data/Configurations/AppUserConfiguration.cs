using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travellin.Core.Entities;
using Travellin.Infrastructure.Data.Seeds;

namespace Travellin.Infrastructure.Data.Configurations
{
    class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasMany(u => u.Roles)
                .WithMany()
                .UsingEntity<IdentityUserRole<string>>(
                    j => j.HasOne<IdentityRole>().WithMany().HasForeignKey(ur => ur.RoleId),
                    j => j.HasOne<AppUser>().WithMany().HasForeignKey(ur => ur.UserId));

            builder.HasData(AppUserSeed.Data);
        }
    }
}
