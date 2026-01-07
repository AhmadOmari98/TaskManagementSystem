using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementSystem.Domain.Constants;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Infrastructure.EntitiesConfigurations
{
    public class UserConfigration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(UserConstraints.NameMaxLength);

            builder.Property(e => e.Email)
                   .IsRequired()
                   .HasMaxLength(UserConstraints.EmailMaxLength);

            builder.HasIndex(e => e.Email)
                   .IsUnique();
        }
    }
}
