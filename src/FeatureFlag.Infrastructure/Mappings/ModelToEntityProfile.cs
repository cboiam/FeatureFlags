using AutoMapper;
using Entities = FeatureFlag.Domain.Models;

namespace FeatureFlag.Infrastructure.Mappings
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
