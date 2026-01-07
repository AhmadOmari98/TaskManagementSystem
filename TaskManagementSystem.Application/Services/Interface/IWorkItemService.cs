using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Application.DTOs.Filter;
using TaskManagementSystem.Application.DTOs.Request;
using TaskManagementSystem.Application.DTOs.Response;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.Services.Interface
{
    public interface IWorkItemService
    {
        Task<PagedDataDto<WorkItemResponse>> Search(SearchPageDto<WorkItemFilter> searchPageDto, int loggedInUserId, UserRole loggedInUserRole);
        Task<WorkItemResponse> GetById(int id, int loggedInUserId, UserRole loggedInUserRole);
        Task Add(CreateWorkItemRequest request, int loggedInUserId);
        Task Update(UpdateWorkItemRequest request, int loggedInUserId, UserRole loggedInUserRole);
        Task UpdateStatus(int id, WorkItemStatus status, int loggedInUserId, UserRole loggedInUserRole);
        Task Delete(int id, int loggedInUserId, UserRole loggedInUserRole);
    }
}
