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
    internal class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        void IEntityTypeConfiguration<Comment>.Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(comment => comment.Id)
                .IsRequired();

            builder.HasKey(comment => comment.Id);

            builder.Property(comment => comment.CreatedAt)
                .IsRequired();

            builder.Property(comment => comment.UpdatedAt)
                .IsRequired();

            builder.Property(comment => comment.UserId)
                .IsRequired();

            builder.Property(comment => comment.PostId)
                .IsRequired();

            builder.Property(comment => comment.Text)
                .IsRequired();

            builder.HasOne(comment => comment.Poster)
                .WithMany(user => user.Comments)
                .HasForeignKey(comment => comment.UserId)
                .HasPrincipalKey(user => user.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(comment => comment.Post)
                .WithMany(post => post.Comments)
                .HasForeignKey(comment => comment.PostId)
                .HasPrincipalKey(post => post.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
