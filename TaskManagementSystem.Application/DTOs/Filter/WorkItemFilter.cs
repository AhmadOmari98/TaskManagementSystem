using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.DTOs.Filter
{
    public class WorkItemFilter
    {
        public string? Title { get; set; }
        public string? ReferenceCode { get; set; }
        public int? AssignedUserId { get; set; }
        public WorkItemStatus? Status { get; set; }
    }
}
