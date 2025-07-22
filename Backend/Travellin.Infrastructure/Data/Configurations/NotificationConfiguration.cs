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
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasConversion(
                    v => v.ToString(),
                    v => v); // Enum conversion if NotificationType is an enum

            builder.Property(x => x.Content)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.IsRead)
                .IsRequired()
                .HasDefaultValue(false);

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.UserId);

            // Seed data
            builder.HasData(
                new Notification
                {
                    Id = 1,
                    UserId = "3dacdb51-fee9-4479-904c-cafe7dca22a7",
                    Name = "NewMessage",
                    Content = "You have a new message from user2@example.com",
                    IsRead = false
                },
                new Notification
                {
                    Id = 2,
                    UserId = "4dacdb51-fee9-4479-904c-cafe7dca22a8",
                    Name = "BookingConfirmation",
                    Content = "Your booking for Cozy Apartment is confirmed",
                    IsRead = false
                }
            );
        }
    }
}
