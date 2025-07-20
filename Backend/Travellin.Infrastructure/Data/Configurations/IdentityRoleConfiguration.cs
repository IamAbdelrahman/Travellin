using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travellin.Infrastructure.Data.Seeds;

namespace Travellin.Infrastructure.Data.Configurations
{
    class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(IdentityRoleSeed.Data);
        }
    }
}
