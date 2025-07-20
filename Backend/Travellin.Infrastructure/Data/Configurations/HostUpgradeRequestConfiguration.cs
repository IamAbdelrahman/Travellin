using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travellin.Core.Entities;
using Travellin.Core.Enums;

namespace Travellin.Infrastructure.Data.Configurations
{
    class HostUpgradeRequestConfiguration : IEntityTypeConfiguration<HostUpgradeRequest>
    {
        public void Configure(EntityTypeBuilder<HostUpgradeRequest> builder)
        {
            builder.HasKey(x => x.Id);

            // ID configuration
            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (HostUgradeRequestStatus)Enum.Parse(typeof(HostUgradeRequestStatus), v))
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.ApprovedBy)
                .IsRequired(false);

            builder.Property(x => x.ApprovalDate)
                .IsRequired(false);

            builder.Property(x => x.RejectionReason)
                .IsRequired(false);

            builder.Property(x => x.DocumentType)
                .HasConversion(
                    v => v.ToString(),
                    v => (HostUpgradeRequestDocumentType)Enum.Parse(typeof(HostUpgradeRequestDocumentType), v))
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.DocumentNumber)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.FrontPhotoId)
                .IsRequired();

            builder.Property(x => x.BackPhotoId)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();

            // Navigation property
            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Approver)
                .WithMany()
                .HasForeignKey(x => x.ApprovedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.FrontPhoto)
                    .WithOne()
                    .HasForeignKey<HostUpgradeRequest>(x => x.FrontPhotoId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.BackPhoto)
                .WithOne()
                .HasForeignKey<HostUpgradeRequest>(x => x.BackPhotoId)
                .OnDelete(DeleteBehavior.Restrict);

            // indexes
            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.Status);
        }
    }
}
