using FeatureFlag.Infrastructure.DbContexts;
using Microsoft.AspNetCore.Builder;
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
                opt.UseMySQL(configuration.GetConnectionString("FeatureFlag"));
            });

            return services;
        }

        public static IApplicationBuilder UseAutomaticMigrations(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var dbContext = serviceScope.ServiceProvider.GetService<FeatureFlagContext>())
                {
                    dbContext.Database.Migrate();
                }
            }

            return app;
        }
    }
}
