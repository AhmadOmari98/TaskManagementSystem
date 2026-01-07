using System.ComponentModel.DataAnnotations;
using TaskManagementSystem.Domain.Constants;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.DTOs.Request
{
    public class CreateWorkItemRequest
    {
        [Required, StringLength(WorkItemConstraints.TitleMaxLength, MinimumLength = 2, ErrorMessage = "Title does not meet length requirements")]
        public string Title { get; set; }
        
        [StringLength(WorkItemConstraints.DescriptionMaxLength, ErrorMessage = "Description does not meet length requirements")]
        public string? Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Assigned user id must be a positive value")]
        public int? AssignedUserId { get; set; }

    }

    public class UpdateWorkItemRequest : CreateWorkItemRequest
    {
        [Required, Range(1, int.MaxValue, ErrorMessage = "Id must be greater than zero")]
        public int Id { get; set; }

        [Required, EnumDataType(typeof(WorkItemStatus))]
        public WorkItemStatus Status { get; private set; }
    }
}
