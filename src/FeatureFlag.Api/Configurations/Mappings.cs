using AutoMapper;
using FeatureFlag.Api.Mappings;
using FeatureFlag.Infrastructure.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace FeatureFlag.Api.Configurations
{
    public static class Mappings
    {
        public static IServiceCollection RegisterMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(EntityToModelProfile),
                typeof(ModelToEntityProfile),
                typeof(RequestToEntityProfile));

            return services;
        }
    }
}
