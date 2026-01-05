using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Domain.Entities
{
    public class User : DomainEntity
    {
        protected User() { }
        public User(string name, string email, UserRoleEnum role, int createdBy)
        {
            Name = name;
            Email = email;
            Role = role;

            CreatedBy = createdBy;
            CreatedDate = DateTime.UtcNow;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public UserRoleEnum Role { get; set; }
        //--------------------------------------------------------*
        internal void Update(string name, string email, UserRoleEnum role, int updatedBy)
        {
            Name = name;
            Email = email;
            Role = role;

            UpdatedBy = updatedBy;
            UpdatedDate = DateTime.UtcNow;
        }
        internal void Delete(int updatedBy)
        {
            IsDeleted = true;

            UpdatedBy = updatedBy;
            UpdatedDate = DateTime.UtcNow;
        }
        internal void Activate(int updatedBy)
        {
            IsActivated = true;

            UpdatedBy = updatedBy;
            UpdatedDate = DateTime.UtcNow;
        }
        internal void Deactivate(int updatedBy)
        {
            IsActivated = false;

            UpdatedBy = updatedBy;
            UpdatedDate = DateTime.UtcNow;
        }
    }
}
