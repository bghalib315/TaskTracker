using Microsoft.Extensions.DependencyInjection;
using TaskTracker.Infrastructure.Bases;
using TaskTracker.Infrastructure.interfaces;
using TaskTracker.Infrastructure.Repositories;

namespace TaskTracker.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepositry>();
            services.AddTransient<ITagRepositry, TagRepositry>();
            services.AddTransient<ITeamRepositry, TeamRepositry>();
            services.AddTransient<ITenantRepositry, TenantRepositry>();
            services.AddTransient<ITaskRepositry, TaskRepositry>();
            services.AddTransient<IRefershTokenRepositry, RefershTokenRepositry>();
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            return services;  
        }

    }
}