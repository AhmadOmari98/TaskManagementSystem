using TaskManagementSystem.Application.DTOs.Request;
using TaskManagementSystem.Application.DTOs.Response;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Mapping
{
    public static class UserMapping
    {
        public static User To(this CreateUserRequest request, int createdBy)
        {
            return new User(name: request.Name,
                            email: request.Email,
                            role: request.Role,
                            createdBy: createdBy);
        }

        public static UserResponse From(this User entity)
        {
            var response = new UserResponse()
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                Role = entity.Role
            };

            return response;
        }
    }
}
