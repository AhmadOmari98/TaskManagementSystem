using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.DTOs.Filter
{
    public class UserFilter
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public UserRole? Role { get; set; }
    }
}
