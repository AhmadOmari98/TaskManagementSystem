using TaskManagementSystem.Infrastructure.Context;

namespace TaskManagementSystem.API.Middlewares
{
    // Automatically saves database changes after each request
    public class SaveChangesMiddleware
    {
        private readonly RequestDelegate _next;

        public SaveChangesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ApplicationDbContext dbContext)
        {
            await _next(context);

            // Save changes only if there are pending changes
            if (dbContext.ChangeTracker.HasChanges())
                await dbContext.SaveChangesAsync();
        }
    }
}
