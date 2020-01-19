using AutoMapper;
using FeatureFlag.Api.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace FeatureFlag.Api.Configurations
{
    public static class Mappings
    {
        public static IServiceCollection RegisterMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(EntityToModelProfile),
                typeof(RequestToEntityProfile),
                typeof(EntityToResponseProfile));

            return services;
        }
    }
}
