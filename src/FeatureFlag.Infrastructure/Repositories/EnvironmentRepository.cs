using AutoMapper;
using FeatureFlag.Application.Interfaces.Repositories;
using FeatureFlag.Domain.Models;
using FeatureFlag.Infrastructure.DbContexts;

namespace FeatureFlag.Infrastructure.Repositories
{
    public class EnvironmentRepository : Repository<Models.Environment, Environment>, IEnvironmentRepository
    {
        public EnvironmentRepository(FeatureFlagContext context, IMapper mapper) 
            : base(context, mapper) { }
    }
}
