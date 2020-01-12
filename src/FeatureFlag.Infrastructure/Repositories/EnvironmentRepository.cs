using AutoMapper;
using FeatureFlag.Application.Interfaces.Repositories;
using FeatureFlag.Domain.Models;
using FeatureFlag.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FeatureFlag.Infrastructure.Repositories
{
    public class EnvironmentRepository : Repository<Models.Environment, Environment>, IEnvironmentRepository
    {
        public EnvironmentRepository(FeatureFlagContext context, IMapper mapper) 
            : base(context, mapper) { }

        public void Update(Environment currentEnvironment, Environment requestedEnvironment)
        {
            var environmentModel = mapper.Map<Models.Environment>(requestedEnvironment);

            environmentModel.Id = currentEnvironment.Id;
            context.Entry(environmentModel).State = EntityState.Modified;
        }
    }
}
