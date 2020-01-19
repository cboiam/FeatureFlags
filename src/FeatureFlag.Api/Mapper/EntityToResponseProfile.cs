using AutoMapper;
using FeatureFlag.Application.Models;
using FeatureFlag.Domain.Models;

namespace FeatureFlag.Api.Mapper
{
    public class EntityToResponseProfile : Profile
    {
        public EntityToResponseProfile()
        {
            CreateMap<Feature, FeatureResponse>();
        }
    }
}
