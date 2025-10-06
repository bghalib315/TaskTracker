using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TaskTracker.Core.Bases;
using TaskTracker.Infrastructure.interfaces;

namespace TaskTracker.Core
{
    public static class ModuleCoreDependencies
    {
        public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<ResponseHandler>();
            //  services.AddAutoMapper(typeof(MappingProfile).Assembly);

            return services;
        }
    }
}