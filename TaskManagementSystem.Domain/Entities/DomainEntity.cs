namespace TaskManagementSystem.Domain.Entities
{
    public class DomainEntity
    {
        public int Id { get; protected set; }

        public int? CreatedBy { get; protected set; } = null;
        public int? UpdatedBy { get; protected set; } = null;

        public DateTime? CreatedDate { get; protected set; } = null;
        public DateTime? UpdatedDate { get; protected set; } = null;

        public bool IsPublished { get; protected set; } = false;
        public bool IsActivated { get; protected set; } = true;
        public bool IsDeleted { get; protected set; } = false;
    }
}
