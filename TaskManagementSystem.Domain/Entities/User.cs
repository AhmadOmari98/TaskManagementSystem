using TaskManagementSystem.Domain.Constants;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Domain.Entities
{
    public class User : DomainEntity
    {
        protected User() { }
        public User(string name, string email, UserRole role, int? loggedInUserId)
        {
            Validate(name: name,
                     email: email,
                     role: role);

            Name = name;
            Email = email;
            Role = role;

            CreatedBy = loggedInUserId;
            CreatedDate = DateTime.Now;
        }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public UserRole Role { get; private set; }
        //--------------------------------------------------------*
        public void Update(string name, string email, UserRole role, int loggedInUserId)
        {
            Validate(name: name,
                     email: email,
                     role: role);

            Name = name;
            Email = email;
            Role = role;

            UpdatedBy = loggedInUserId;
            UpdatedDate = DateTime.Now;
        }
        public void Delete(int loggedInUserId)
        {
            IsDeleted = true;

            UpdatedBy = loggedInUserId;
            UpdatedDate = DateTime.Now;
        }
        //--------------------------------------------------------*
        private void Validate(string name, string email, UserRole role)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Name is required");

            if (name.Length > UserConstraints.NameMaxLength)
                throw new Exception($"Name cannot exceed {UserConstraints.NameMaxLength} characters");

            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("Email is required");

            if (email.Length > UserConstraints.EmailMaxLength)
                throw new Exception($"Email cannot exceed {UserConstraints.EmailMaxLength} characters");

            if (!Enum.IsDefined(typeof(UserRole), role))
                throw new Exception("Invalid user role");
        }

    }
}
