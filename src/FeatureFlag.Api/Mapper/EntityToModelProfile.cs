using AutoMapper;
using Entities = FeatureFlag.Domain.Models;
using Models = FeatureFlag.Infrastructure.Models;

namespace FeatureFlag.Api.Mapper
{
    public class EntityToModelProfile : Profile
    {
        public EntityToModelProfile()
        {
            CreateMap<Entities.Feature, Models.Feature>()
                .ReverseMap();

            CreateMap<Entities.Environment, Models.Environment>()
                .ReverseMap();

            CreateMap<Entities.User, Models.User>()
                .ReverseMap();
        }
    }
}