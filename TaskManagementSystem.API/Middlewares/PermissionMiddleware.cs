using TaskManagementSystem.API.Attributes;
using TaskManagementSystem.Application.Authorization;
using TaskManagementSystem.Domain.Enums;

public class PermissionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<PermissionMiddleware> _logger;

    public PermissionMiddleware(RequestDelegate next, ILogger<PermissionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Read headers
        var loggedInUserIdHeader = context.Request.Headers["X-User-Id"].FirstOrDefault();
        var loggedInUserRoleHeader = context.Request.Headers["X-User-Role"].FirstOrDefault();

        // Validate headers existence
        if (string.IsNullOrWhiteSpace(loggedInUserIdHeader) || string.IsNullOrWhiteSpace(loggedInUserRoleHeader))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Missing user headers");
            return;
        }

        // Parse role
        if (!Enum.TryParse<UserRole>(loggedInUserRoleHeader, true, out var role))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid role");
            return;
        }

        // Store user context
        context.Items["LoggedInUserId"] = int.Parse(loggedInUserIdHeader);
        context.Items["LoggedInUserRole"] = role;

        _logger.LogInformation($"Request by UserId={loggedInUserIdHeader}, Role={role}");

        // Get required permission from endpoint
        var endpoint = context.GetEndpoint();
        var permissionAttr = endpoint?.Metadata.GetMetadata<RequirePermissionAttribute>();

        // Check permission
        if (permissionAttr != null)
        {
            if (!RolePermissions.Map.TryGetValue(role, out var permissions) || !permissions.Contains(permissionAttr.Permission))
            {
                _logger.LogWarning($"Permission denied. UserId={loggedInUserIdHeader}, Role={role}, Permission={permissionAttr.Permission}");

                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Access denied");
                return;
            }
        }

        await _next(context);
    }
}