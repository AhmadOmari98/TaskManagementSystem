using Microsoft.Extensions.DependencyInjection;
using TaskManagementSystem.Domain.Interface.Repositories;
using TaskManagementSystem.Infrastructure.Repositories;

namespace TaskManagementSystem.Infrastructure
{
    public static class DependencyInjection_AddRepositories
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        }
    }
}
