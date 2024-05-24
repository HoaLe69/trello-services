using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using trello_services.Entities;

namespace trello_services.Configuration.Entities
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");
            builder.HasKey(u => u.userId);
            builder.Property(u => u.displayName).IsRequired(false);
            builder.Property(u => u.email).HasMaxLength(256).IsRequired();
            builder.HasIndex(u => u.email).IsUnique();
            builder.Property(u => u.avatar_path).IsRequired(false);
        }
    }
}
