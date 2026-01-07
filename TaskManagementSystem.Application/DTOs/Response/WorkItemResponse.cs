using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.DTOs.Response
{
    public class WorkItemResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public WorkItemStatus Status { get; set; }
        public string ReferenceCode { get; set; }
        public int? AssignedUserId { get; set; }
        public UserResponse? AssignedUser { get; set; }
    }
}
