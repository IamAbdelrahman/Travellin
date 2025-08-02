using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travellin.Core.Entities;
using Travellin.Infrastructure.Data.Seeds;

namespace Travellin.Infrastructure.Data.Configurations
{
     class RecommendationsConfigruations: IEntityTypeConfiguration<Recommendations>
    {
        public void Configure(EntityTypeBuilder<Recommendations> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.UserId)
                   .IsRequired();

            builder.Property(x => x.PropertyId)
                   .IsRequired();

            builder.Property(x => x.Query)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(x => x.Score)
                   .HasColumnType("decimal(5,2)")
                   .IsRequired();

            builder.Property(x => x.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
