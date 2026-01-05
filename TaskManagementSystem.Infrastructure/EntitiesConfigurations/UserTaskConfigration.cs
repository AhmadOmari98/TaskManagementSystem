using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Infrastructure.EntitiesConfigurations
{
    public class UserTaskConfigration : IEntityTypeConfiguration<UserTask>
    {
        public void Configure(EntityTypeBuilder<UserTask> builder)
        {
            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Title)
                   .IsRequired()
                   .HasMaxLength(250);

            builder.Property(x => x.Description)
                   .IsRequired()
                   .HasMaxLength(1500);

            builder.Property(x => x.Status)
                   .IsRequired();

            builder.HasOne(x => x.AssignedUser)
                   .WithMany()
                   .HasForeignKey(x => x.AssignedUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.AssignedUserId);
        }
    }
}
