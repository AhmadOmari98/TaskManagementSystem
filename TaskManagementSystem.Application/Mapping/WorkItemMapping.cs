using TaskManagementSystem.Application.DTOs.Request;
using TaskManagementSystem.Application.DTOs.Response;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Mapping
{
    public static class WorkItemMapping
    {
        public static WorkItem To(this CreateWorkItemRequest request, int loggedInUserId)
        {
            return new WorkItem(title: request.Title,
                                description: request.Description,
                                assignedUserId: request.AssignedUserId,
                                loggedInUserId: loggedInUserId);
        }

        public static WorkItemResponse From(this WorkItem entity)
        {
            var response = new WorkItemResponse()
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Status = entity.Status,
                ReferenceCode = entity.ReferenceCode,
                AssignedUserId = entity.AssignedUserId,
                AssignedUser = entity.AssignedUser?.From() ?? null
            };

            return response;
        }
    }
}
