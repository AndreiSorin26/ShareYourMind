using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.EntityConfigurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        void IEntityTypeConfiguration<Post>.Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(post => post.Id)
                .IsRequired();

            builder.Property(post => post.CreatedAt)
                .IsRequired();

            builder.Property(post => post.UpdatedAt)
                .IsRequired();

            builder.Property(post => post.UserId)
                .IsRequired();

            builder.Property(post => post.Text)
                .IsRequired();

            builder.HasOne(post => post.Poster)
                .WithMany(user => user.Posts)
                .HasForeignKey(post => post.UserId)
                .HasPrincipalKey(user => user.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(post => post.LoveReactionUsers)
                .WithMany(user => user.LoveReactionPosts)
                .UsingEntity("LoveReactions");

            builder.HasMany(post => post.LaughReactionUsers)
                .WithMany(user => user.LaughReactionPosts)
                .UsingEntity("LaughReactions");

            builder.HasMany(post => post.DislikeReactionUsers)
                .WithMany(user => user.DislikeReactionPosts)
                .UsingEntity("DislikeReactions");
        }
    }
}
