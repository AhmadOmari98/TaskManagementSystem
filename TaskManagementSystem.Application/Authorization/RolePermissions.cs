using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.Authorization
{
    public static class RolePermissions
    {
        public static readonly Dictionary<UserRole, List<string>> Map =
            new()
            {
                [UserRole.Admin] = new()
                {
                    Permissions.User_Search,
                    Permissions.User_View,
                    Permissions.User_Create,
                    Permissions.User_Update,
                    Permissions.User_Delete,

                    Permissions.WorkItem_Search,
                    Permissions.WorkItem_View,
                    Permissions.WorkItem_Create,
                    Permissions.WorkItem_Update,
                    Permissions.WorkItem_UpdateStatus,
                    Permissions.WorkItem_Delete
                },

                [UserRole.User] = new()
                {
                    Permissions.User_View,

                    Permissions.WorkItem_Search,
                    Permissions.WorkItem_View,
                    Permissions.WorkItem_UpdateStatus,
                }
            };
    }
}
