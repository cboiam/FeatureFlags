using FeatureFlag.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FeatureFlag.Infrastructure.Extensions
{
    public static class DbContextExtension
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FeatureFlagContext>(opt =>
            {
                opt.UseMySql(configuration.GetConnectionString("FeatureFlag"));
            });

            return services;
        }
    }
}
