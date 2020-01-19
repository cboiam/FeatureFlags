using FeatureFlag.Application.Models;
using FeatureFlag.Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FeatureFlag.Api.Configurations
{
    public static class ValidatorDependencies
    {
        public static IServiceCollection RegisterValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<FeaturePostRequest>, FeaturePostRequestValidator>();
            services.AddScoped<IValidator<FeaturePutRequest>, FeaturePutRequestValidator>();

            return services;
        }
    }
}
