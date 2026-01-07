using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.DTOs.Response
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
    }
}
