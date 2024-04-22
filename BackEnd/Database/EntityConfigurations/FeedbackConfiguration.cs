using Database.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.EntityConfigurations
{
    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.Property(feedback => feedback.Id)
                .IsRequired();
            builder.HasKey(feedback => feedback.Id);

            builder.Property(feedback => feedback.CreatedAt)
                .IsRequired();

            builder.Property(feedback => feedback.UpdatedAt)
                .IsRequired();

            builder.Property(feedback => feedback.UserId)
                .IsRequired();

            builder.Property(feedback => feedback.Text)
                .HasDefaultValue(String.Empty)
                .IsRequired();

            builder.Property(feedback => feedback.UIRating)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(feedback => feedback.UXRating)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(feedback => feedback.CommunityRating)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(feedback => feedback.DataFlowRating)
                .HasDefaultValue(0)
                .IsRequired();

            builder.HasOne(feedback => feedback.User)
                .WithMany(user => user.Feedbacks)
                .HasForeignKey(feedback => feedback.UserId)
                .HasPrincipalKey(user => user.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
