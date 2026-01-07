namespace TaskManagementSystem.API.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RequirePermissionAttribute : Attribute
    {
        public string Permission { get; }
        public RequirePermissionAttribute(string permission)
        {
            Permission = permission;
        }
    }
}
