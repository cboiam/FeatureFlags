using AutoMapper;
using FeatureFlag.Application.Models;
using FeatureFlag.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace FeatureFlag.Api.Mapper
{
    public class RequestToEntityProfile : Profile
    {
        public RequestToEntityProfile()
        {
            CreateMap<FeaturePostRequest, Feature>()
                .ForMember(dest => dest.Environments, opt => opt.MapFrom(src => new List<Environment>
                {
                    new Environment
                    {
                        Enabled = src.Enabled,
                        UsersEnabled = src.EnabledUserNames.Select(n => new User { Name = n })
                    }
                }));

            CreateMap<FeaturePutRequest, Feature>()
                .ForMember(dest => dest.Environments, opt => opt.MapFrom(src => new List<Environment> 
                {
                    new Environment
                    {
                        Enabled = src.Enabled,
                        UsersEnabled = src.EnabledUserNames.Select(n => new User { Name = n })
                    }
                }));
        }
    }
}
