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
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.ConversationId)
                .IsRequired();

            builder.Property(x => x.SenderId)
                .IsRequired()
                .HasMaxLength(450); // Assuming string ID length (e.g., GUID or username)

            builder.Property(x => x.ReceiverId)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(x => x.Content)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.TranslatedContent)
                .HasMaxLength(1000);

            builder.Property(x => x.IsRead)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(x => x.SentAt)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(x => x.Conversation)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.ConversationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Sender)
                .WithMany()
                .HasForeignKey(x => x.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Receiver)
                .WithMany()
                .HasForeignKey(x => x.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.ConversationId);
            builder.HasIndex(x => x.SenderId);
            builder.HasIndex(x => x.ReceiverId);
            builder.HasIndex(x => x.SentAt);

            // Seed data
            builder.HasData(
                new Message
                {
                    Id = 1,
                    ConversationId = 1,
                    SenderId = "4dacdb51-fee9-4479-904c-cafe7dca22a8",
                    ReceiverId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                    Content = "Hello, is the property available?",
                    IsRead = false,
                    SentAt = DateTime.Parse("2025-05-16T14:30:00Z")
                },
                new Message
                {
                    Id = 2,
                    ConversationId = 1,
                    SenderId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                    ReceiverId = "4dacdb51-fee9-4479-904c-cafe7dca22a8",
                    Content = "Yes, it’s available next week!",
                    IsRead = false,
                    SentAt = DateTime.Parse("2025-03-16T14:30:00Z")
                }
            );
        }
    }
}
