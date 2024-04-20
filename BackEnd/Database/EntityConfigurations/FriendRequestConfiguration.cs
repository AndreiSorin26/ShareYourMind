using Database.Entities;
using Database.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.EntityConfigurations
{
    public class FriendRequestConfiguration : IEntityTypeConfiguration<FriendRequest>
    {
        void IEntityTypeConfiguration<FriendRequest>.Configure(EntityTypeBuilder<FriendRequest> builder)
        {
            builder.Property(friendRequest => friendRequest.Id)
                .IsRequired();

            builder.HasAlternateKey(friendRequest => new { friendRequest.SenderId, friendRequest.ReceiverId });

            builder.Property(friendRequest => friendRequest.UpdatedAt)
                .IsRequired();

            builder.Property(friendRequest => friendRequest.CreatedAt)
                .IsRequired();

            builder.Property(friendRequest => friendRequest.SenderId)
                .IsRequired();

            builder.Property(friendRequest => friendRequest.ReceiverId)
                .IsRequired();

            builder.Property(friendRequest => friendRequest.Status)
                .HasConversion(status => status.ToString(), status => (FriendRequestStatus)Enum.Parse(typeof(FriendRequestStatus), status))
                .IsRequired();

            builder.HasOne(friendRequest => friendRequest.Sender)
                .WithMany(user => user.SentFriendRequests)
                .HasForeignKey(friendRequest => friendRequest.SenderId)
                .HasPrincipalKey(user => user.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(friendRequest => friendRequest.Receiver)
                .WithMany(user => user.ReceivedFriendRequests)
                .HasForeignKey(friendRequest => friendRequest.ReceiverId)
                .HasPrincipalKey(user => user.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
