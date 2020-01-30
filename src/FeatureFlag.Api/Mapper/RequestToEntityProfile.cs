using AutoMapper;
using FeatureFlag.Application.Models;
using FeatureFlag.Domain.Models;
using System.Linq;

namespace FeatureFlag.Api.Mapper
{
    public class RequestToEntityProfile : Profile
    {
        public RequestToEntityProfile()
        {
            CreateMap<FeaturePostRequest, Feature>();
            CreateMap<FeaturePutRequest, Feature>();

            CreateMap<EnvironmentPostRequest, Environment>()
                .ForMember(dest => dest.UsersEnabled, 
                           opt => opt.MapFrom(src => src.UsersEnabled.Select(n => new User { Name = n })));

            CreateMap<EnvironmentPutRequest, Environment>()
                .ForMember(dest => dest.UsersEnabled, 
                           opt => opt.MapFrom(src => src.UsersEnabled.Select(n => new User { Name = n })));
        }
    }
}
