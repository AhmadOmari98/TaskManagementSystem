using Microsoft.Extensions.DependencyInjection;
using TaskManagementSystem.Application.Services.Implementation;
using TaskManagementSystem.Application.Services.Interface;

namespace TaskManagementSystem.Application
{
    public static class DependencyInjection_AddServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddScoped<IUserService,UserService>()
                           .AddScoped<IWorkItemService, WorkItemService>();
        }
    }
}
