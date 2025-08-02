using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Travellin.Core.Entities;

namespace Travellin.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Configures the database schema for the Conversation entity using Entity Framework Core's fluent API.
    /// </summary>
    public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
    {
        /// <summary>
        /// Applies the configuration for the Conversation entity.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<Conversation> builder)
        {
            // Configure the primary key
            builder.HasKey(x => x.Id);

            // Configure the Id property
            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            // Configure the User1Id property
            builder.Property(x => x.User1Id)
                .IsRequired()
                .HasMaxLength(450);

            // Configure the User2Id property
            builder.Property(x => x.User2Id)
                .IsRequired()
                .HasMaxLength(450);

            // Configure the PropertyId property as optional
            builder.Property(x => x.PropertyId)
                .HasMaxLength(450)
                .IsRequired(false);

            // Configure the CreatedAt property with a default SQL value
            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            // Configure the relationship with the first user (User1)
            builder.HasOne(x => x.User1)
                .WithMany()
                .HasForeignKey(x => x.User1Id)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the relationship with the second user (User2)
            builder.HasOne(x => x.User2)
                .WithMany()
                .HasForeignKey(x => x.User2Id)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the optional relationship with the Property
            builder.HasOne(x => x.Property)
                .WithMany()
                .HasForeignKey(x => x.PropertyId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the relationship with Messages
            builder.HasMany(x => x.Messages)
                .WithOne()
                .HasForeignKey(m => m.ConversationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Add indexes for foreign keys to improve query performance
            builder.HasIndex(x => x.User1Id);
            builder.HasIndex(x => x.User2Id);
            builder.HasIndex(x => x.PropertyId);

            // Seed data for the Conversation entity
            builder.HasData(
                new Conversation
                {
                    Id = 1,
                    User1Id = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                    User2Id = "4dacdb51-fee9-4479-904c-cafe7dca22a8",
                    CreatedAt = new DateTime(2024, 12, 12)
                }
            );
        }
    }
}
