using Database.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.EntityConfigurations
{
    internal class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        void IEntityTypeConfiguration<Report>.Configure(EntityTypeBuilder<Report> builder)
        {
            builder.Property(report => report.Id)
                .IsRequired();

            builder.HasAlternateKey(report => new { report.ReportingUserId, report.PostId });

             builder.Property(report => report.CreatedAt)
                .IsRequired();

            builder.Property(report => report.UpdatedAt)
                .IsRequired();

            builder.Property(report => report.ReportingUserId)
                .IsRequired();

            builder.Property(report => report.PostId)
                .IsRequired();

            builder.Property(report => report.Text)
                .IsRequired();

            builder.Property(report => report.Closed)
                .IsRequired();

            builder.HasOne(report => report.Reporter)
                .WithMany(user => user.Reports)
                .HasForeignKey(report => report.ReportingUserId)
                .HasPrincipalKey(user => user.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(report => report.Post)
                .WithMany(post => post.Reports)
                .HasForeignKey(report => report.PostId)
                .HasPrincipalKey(post => post.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}