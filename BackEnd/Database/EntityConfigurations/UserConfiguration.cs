using Database.Entities;
using Database.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.EntityConfigurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        void IEntityTypeConfiguration<User>.Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(user => user.Id)
                .IsRequired();

            builder.HasAlternateKey(user => user.DisplayName);

            builder.Property(user => user.UpdatedAt)
                .IsRequired();

            builder.Property(user => user.CreatedAt)
                .IsRequired();

            builder.Property(user => user.Username)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(user => user.Password)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(user => user.Salt)
                .IsRequired();

            builder.Property(user => user.Role)
                .HasConversion(role => role.ToString(), role => (UserRole)Enum.Parse(typeof(UserRole), role))
                .IsRequired();

            builder.Property(user => user.Tag)
                .IsRequired();

            builder.Property(user => user.DisplayName)
                .IsRequired();
            builder.HasAlternateKey(user => user.DisplayName);

            builder.Property(user => user.Approved)
                .IsRequired();
        }
    }
}
