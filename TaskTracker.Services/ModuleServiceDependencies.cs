using Microsoft.Extensions.DependencyInjection;
using TaskTracker.Infrastructure.interfaces;
using TaskTracker.Services.abstracts;
using TaskTracker.Services.Repository;

namespace TaskTracker.Services
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IUserServices, UserService>();
            services.AddTransient<ITeamServices, TeamService>();
            services.AddTransient<ITenantServices, TenantService>();
            services.AddTransient<ITaskServices, TaskService>();
            services.AddTransient<ITagServices, TagService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IAuthorizationService, AuthorizationService>();
            return services;
        }
    }
}