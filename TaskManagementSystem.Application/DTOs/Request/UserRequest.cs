
using System.ComponentModel.DataAnnotations;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.DTOs.Request
{
    public class CreateUserRequest
    {
        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, EnumDataType(typeof(UserRoleEnum))]
        public UserRoleEnum Role { get; set; }
    }

    public class UpdateUserRequest: CreateUserRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
