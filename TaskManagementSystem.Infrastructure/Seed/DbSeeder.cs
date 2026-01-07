using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Enums;
using TaskManagementSystem.Infrastructure.Context;

namespace TaskManagementSystem.Infrastructure.Seed
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.Set<User>().Any())
                SeedUsers(context);

            if (!context.Set<WorkItem>().Any())
                SeedWorkItems(context);
        }

        private static void SeedUsers(ApplicationDbContext context)
        {
            if (context.Set<User>().Any())
                return;

            var admin = new User(
                name: "Admin User",
                email: "admin@test.com",
                role: UserRole.Admin,
                loggedInUserId: null);

            var normalUser = new User(
                name: "Normal User",
                email: "user@test.com",
                role: UserRole.User,
                loggedInUserId: null);

            context.Set<User>().AddRange(admin, normalUser);
            context.SaveChanges();
        }

        private static void SeedWorkItems(ApplicationDbContext context)
        {
            if (context.Set<WorkItem>().Any())
                return;

            var admin = context.Set<User>().First(x => x.Role == UserRole.Admin);
            var normalUser = context.Set<User>().First(x => x.Role == UserRole.User);

            var workItems = new List<WorkItem>
            {
                new WorkItem(
                    title: "Setup project",
                    description: "Initial project setup",
                    assignedUserId: admin.Id,
                    loggedInUserId: admin.Id),
            
                new WorkItem(
                    title: "Create users module",
                    description: "Implement users CRUD",
                    assignedUserId: normalUser.Id,
                    loggedInUserId: admin.Id),
            
                new WorkItem(
                    title: "Create tasks module",
                    description: "Implement tasks CRUD",
                    assignedUserId: normalUser.Id,
                    loggedInUserId: admin.Id)
            };

            context.Set<WorkItem>().AddRange(workItems);
            context.SaveChanges();
        }
    }
}
