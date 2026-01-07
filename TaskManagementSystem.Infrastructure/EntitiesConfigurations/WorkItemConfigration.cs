using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementSystem.Domain.Constants;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Infrastructure.EntitiesConfigurations
{
    public class WorkItemConfigration : IEntityTypeConfiguration<WorkItem>
    {
        public void Configure(EntityTypeBuilder<WorkItem> builder)
        {
            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Title)
                   .IsRequired()
                   .HasMaxLength(WorkItemConstraints.TitleMaxLength);

            builder.Property(x => x.Description)
                   .HasMaxLength(WorkItemConstraints.DescriptionMaxLength)
                   .IsRequired(false);

            builder.Property(x => x.Status)
                   .IsRequired();

            builder.Property(x => x.ReferenceCode)
                   .IsRequired()
                   .HasMaxLength(WorkItemConstraints.ReferenceCodeMaxLength);

            builder.HasOne(x => x.AssignedUser)
                              .WithMany()
                              .HasForeignKey(x => x.AssignedUserId)
                              .IsRequired(false)
                              .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.AssignedUserId);
        }
    }
}
