using AutoMapper;
using FeatureFlag.Application.Models;
using FeatureFlag.Domain.Models;

namespace FeatureFlag.Api.Mappings
{
    public class RequestToEntityProfile : Profile
    {
        public RequestToEntityProfile()
        {
            CreateMap<FeaturePostRequest, Feature>();
            CreateMap<FeaturePutRequest, Feature>();

            CreateMap<EnvironmentPostRequest, Environment>();
            CreateMap<EnvironmentPutRequest, Environment>();

            CreateMap<UserRequest, User>();
        }
    }
}
