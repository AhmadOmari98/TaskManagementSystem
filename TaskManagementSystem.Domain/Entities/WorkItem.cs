using TaskManagementSystem.Domain.Constants;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Domain.Entities
{
    public class WorkItem : DomainEntity
    {
        protected WorkItem() { }
        public WorkItem(string title, string? description, int? assignedUserId, int? loggedInUserId)
        {
            Validate(title: title,
                     description: description,
                     status: WorkItemStatus.New,
                     assignedUserId: assignedUserId);

            Title = title;
            Description = description;
            AssignedUserId = assignedUserId;

            Status = WorkItemStatus.New;
            GenerateReferenceCode();

            CreatedBy = loggedInUserId;
            CreatedDate = DateTime.Now;
        }

        public string Title { get; private set; }
        public string? Description { get; private set; }
        public WorkItemStatus Status { get; private set; }
        public string ReferenceCode { get; private set; }
        public int? AssignedUserId { get; private set; }
        public virtual User? AssignedUser { get; private set; }
        //--------------------------------------------------------*
        public void Update(string title, string? description, WorkItemStatus status, int? assignedUserId, int loggedInUserId)
        {
            Validate(title: title,
                     description: description,
                     status: WorkItemStatus.New,
                     assignedUserId: assignedUserId);

            Title = title;
            Description = description;
            AssignedUserId = assignedUserId;
            Status = status;

            UpdatedBy = loggedInUserId;
            UpdatedDate = DateTime.Now;
        }
        public void UpdateStatus(WorkItemStatus status, int loggedInUserId)
        {
            ValidateStatus(status: status);

            Status = status;

            UpdatedBy = loggedInUserId;
            UpdatedDate = DateTime.Now;
        }
        public void Delete(int loggedInUserId)
        {
            IsDeleted = true;

            UpdatedBy = loggedInUserId;
            UpdatedDate = DateTime.Now;
        }
        //--------------------------------------------------------*
        private void GenerateReferenceCode()
        {
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            var randomPart = Guid.NewGuid().ToString("N")[..7].ToUpper();

            ReferenceCode = $"WI-{timestamp}-{randomPart}";
        }
        private static void Validate(string title, string? description, WorkItemStatus status, int? assignedUserId)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new Exception("Title is required");

            if (title.Length > WorkItemConstraints.TitleMaxLength)
                throw new Exception($"Title cannot exceed {WorkItemConstraints.TitleMaxLength} characters");

            if (description is not null && description.Length > WorkItemConstraints.DescriptionMaxLength)
                throw new Exception($"Description cannot exceed {WorkItemConstraints.DescriptionMaxLength} characters");

            // Assigned user is optional, but if provided it must be valid
            if (assignedUserId.HasValue && assignedUserId <= 0)
                throw new Exception("Assigned user id must be a positive value");

            ValidateStatus(status: status);
        }

        private static void ValidateStatus(WorkItemStatus status)
        {
            if (!Enum.IsDefined(typeof(WorkItemStatus), status))
                throw new Exception("Invalid workItem status");
        }
    }
}
