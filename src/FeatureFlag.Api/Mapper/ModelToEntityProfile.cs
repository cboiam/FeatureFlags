using AutoMapper;
using Entities = FeatureFlag.Domain.Models;
using Models = FeatureFlag.Infrastructure.Models;

namespace FeatureFlag.Api.Mapper
{
    public class ModelToEntityProfile : Profile
    {
        public ModelToEntityProfile()
        {
            CreateMap<Models.Feature, Entities.Feature>();

            CreateMap<Models.Environment, Entities.Environment>();

            CreateMap<Models.User, Entities.User>();
        }
    }
}
