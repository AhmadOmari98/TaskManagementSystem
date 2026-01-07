using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.DTOs.Filter
{
    public class WorkItemFilter
    {
        public string? Title { get; private set; }
        public string? ReferenceCode { get; private set; }
        public int? AssignedUserId { get; private set; }
        public WorkItemStatus? Status { get; private set; }
    }
}
