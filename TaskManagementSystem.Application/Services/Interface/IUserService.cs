using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Application.DTOs.Filter;
using TaskManagementSystem.Application.DTOs.Request;
using TaskManagementSystem.Application.DTOs.Response;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.Services.Interface
{
    public interface IUserService
    {
        Task<PagedDataDto<UserResponse>> Search(SearchPageDto<UserFilter> searchPageDto);
        Task<UserResponse> GetById(int id, int loggedInUserId, UserRole loggedInUserRole);
        Task Add(CreateUserRequest request, int loggedInUserId);
        Task Update(UpdateUserRequest request, int loggedInUserId);
        Task Delete(int id, int loggedInUserId);
    }
}
