using AutoMapper;
using System.Linq;
using Entities = FeatureFlag.Domain.Models;
using Models = FeatureFlag.Infrastructure.Models;

namespace FeatureFlag.Api.Mapper
{
    public class EntityToModelProfile : Profile
    {
        public EntityToModelProfile()
        {
            CreateMap<Entities.Feature, Models.Feature>()
                .ForMember(dest => dest.Environments, opt => opt.MapFrom(src => src.Environments.Select(e =>
                    new Models.Environment
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Enabled = e.Enabled,
                        UsersEnabled = e.UsersEnabled.Select(u => new Models.User
                        {
                            Id = u.Id,
                            Name = u.Name,
                            EnvironmentId = e.Id
                        }).ToList(),
                        FeatureId = src.Id
                    }).ToList()
                ));

            CreateMap<Entities.Environment, Models.Environment>()
                .ForMember(dest => dest.UsersEnabled, opt => opt.MapFrom(src => src.UsersEnabled.Select(u => new Models.User
                {
                    Id = u.Id,
                    Name = u.Name,
                    EnvironmentId = src.Id
                })));

            CreateMap<Entities.User, Models.User>();
        }
    }
}