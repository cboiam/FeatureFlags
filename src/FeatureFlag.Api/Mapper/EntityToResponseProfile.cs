using AutoMapper;
using FeatureFlag.Application.Models;
using FeatureFlag.Domain.Models;
using System.Linq;

namespace FeatureFlag.Api.Mapper
{
    public class EntityToResponseProfile : Profile
    {
        public EntityToResponseProfile()
        {
            CreateMap<Feature, FeatureResponse>()
                .ForMember(dest => dest.EnabledUserNames, opt => opt.MapFrom(src =>
                    src.Environments.SelectMany(e => e.UsersEnabled.Select(u => u.Name))));
        }
    }
}
