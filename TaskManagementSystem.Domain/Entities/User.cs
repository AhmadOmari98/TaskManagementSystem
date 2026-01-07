using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Domain.Entities
{
    public class User : DomainEntity
    {
        protected User() { }
        public User(string name, string email, UserRoleEnum role, int createdBy)
        {
            Validate(name, email, role);

            Name = name;
            Email = email;
            Role = role;

            CreatedBy = createdBy;
            CreatedDate = DateTime.UtcNow;
        }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public UserRoleEnum Role { get; private set; }
        //--------------------------------------------------------*
        public void Update(string name, string email, UserRoleEnum role, int updatedBy)
        {
            Validate(name, email, role);

            Name = name;
            Email = email;
            Role = role;

            UpdatedBy = updatedBy;
            UpdatedDate = DateTime.UtcNow;
        }
        public void Delete(int updatedBy)
        {
            IsDeleted = true;

            UpdatedBy = updatedBy;
            UpdatedDate = DateTime.UtcNow;
        }
        public void Activate(int updatedBy)
        {
            IsActivated = true;

            UpdatedBy = updatedBy;
            UpdatedDate = DateTime.UtcNow;
        }
        public void Deactivate(int updatedBy)
        {
            IsActivated = false;

            UpdatedBy = updatedBy;
            UpdatedDate = DateTime.UtcNow;
        }
        //--------------------------------------------------------*
        private void Validate(string name, string email, UserRoleEnum role)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required");

            if (!Enum.IsDefined(typeof(UserRoleEnum), role))
                throw new ArgumentException("Invalid user role");
        }

    }
}
