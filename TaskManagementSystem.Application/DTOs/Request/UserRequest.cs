
using System.ComponentModel.DataAnnotations;
using TaskManagementSystem.Domain.Constants;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.DTOs.Request
{
    public class CreateUserRequest
    {
        [Required, StringLength(UserConstraints.NameMaxLength, MinimumLength = 2, ErrorMessage = "Name does not meet length requirements")]
        public string Name { get; set; }

        [Required, EmailAddress, StringLength(UserConstraints.EmailMaxLength, ErrorMessage = "Email does not meet length requirements")]
        public string Email { get; set; }

        [Required, EnumDataType(typeof(UserRole))]
        public UserRole Role { get; set; }
    }

    public class UpdateUserRequest: CreateUserRequest
    {
        [Required, Range(1, int.MaxValue, ErrorMessage = "Id must be greater than zero")]
        public int Id { get; set; }
    }
}
