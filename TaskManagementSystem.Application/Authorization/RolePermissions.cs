using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.Authorization
{
    public static class RolePermissions
    {
        public static readonly Dictionary<UserRoleEnum, List<string>> Map =
            new()
            {
                [UserRoleEnum.Admin] = new()
                {
                    Permissions.User_Create,
                    Permissions.User_View,
                    Permissions.User_Update,
                    Permissions.User_Delete,

                    Permissions.Task_Create,
                    Permissions.Task_View,
                    Permissions.Task_Update,
                    Permissions.Task_Delete
                },

                [UserRoleEnum.User] = new()
                {
                    Permissions.User_View,
                    Permissions.Task_View,
                    Permissions.Task_Update // status only (checked later)
                }
            };
    }
}
