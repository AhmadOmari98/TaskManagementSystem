
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Domain.Entities
{
    public class UserTask : DomainEntity
    {
        protected UserTask() { }
        public UserTask(string title, string description, int assignedUserId, int createdBy)
        {
            Title = title;
            Description = description;
            AssignedUserId = assignedUserId;

            Status = TaskStatusEnum.New;

            CreatedBy = createdBy;
            CreatedDate = DateTime.UtcNow;
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public TaskStatusEnum Status { get; private set; }
        public int AssignedUserId { get; private set; }
        public virtual User AssignedUser { get; private set; }
        //--------------------------------------------------------*
        public void Update(string title, string description, TaskStatusEnum status, int assignedUserId, int updatedBy)
        {
            Title = title;
            Description = description;
            AssignedUserId = assignedUserId;
            Status = status;

            UpdatedBy = updatedBy;
            UpdatedDate = DateTime.UtcNow;
        }
        public void UpdateStatus(TaskStatusEnum status, int updatedBy)
        {
            Status = status;

            UpdatedBy = updatedBy;
            UpdatedDate = DateTime.UtcNow;
        }
        public void Delete(int updatedBy)
        {
            IsDeleted = true;

            UpdatedBy = updatedBy;
            UpdatedDate = DateTime.UtcNow;
        }
    }
}
