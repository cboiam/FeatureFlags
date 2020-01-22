using FeatureFlag.Application.AppServices;
using FeatureFlag.Application.Interfaces.AppServices;
using FeatureFlag.Application.Interfaces.Repositories;
using FeatureFlag.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FeatureFlag.Api.Configurations
{
    public static class Dependencies
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            services.AddAppServices()
                .AddRepositories();            

            return services;
        }

        private static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IFeatureAppService, FeatureAppService>();
            services.AddScoped<IEnvironmentAppService, EnvironmentAppService>();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IFeatureRepository, FeatureRepository>();
            services.AddScoped<IEnvironmentRepository, EnvironmentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
