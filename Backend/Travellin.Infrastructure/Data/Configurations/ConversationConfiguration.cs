using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travellin.Core.Entities;

namespace Travellin.Infrastructure.Data.Configurations
{
    public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.User1Id)
                .IsRequired()
                .HasMaxLength(450); // Assuming string ID length

            builder.Property(x => x.User2Id)
                .IsRequired()
                .HasMaxLength(450);

            builder.HasOne(x => x.User1)
                .WithMany()
                .HasForeignKey(x => x.User1Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.User2)
                .WithMany()
                .HasForeignKey(x => x.User2Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Messages)
                .WithOne()
                .HasForeignKey(m => m.ConversationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.User1Id);
            builder.HasIndex(x => x.User2Id);

            // Seed data
            builder.HasData(
                new Conversation
                {
                    Id = 1,
                    User1Id = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                    User2Id = "4dacdb51-fee9-4479-904c-cafe7dca22a8"
                }
            );
        }
    }
}
