using AutoMapper;
using FeatureFlag.Application.Interfaces.Repositories;
using FeatureFlag.Domain.Models;
using FeatureFlag.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FeatureFlag.Infrastructure.Repositories
{
    public class EnvironmentRepository : Repository<Models.Environment, Environment>, IEnvironmentRepository
    {
        public EnvironmentRepository(FeatureFlagContext context, IMapper mapper) 
            : base(context, mapper) { }

        public void Update(Environment currentEnvironment, Environment requestedEnvironment, int featureId)
        {
            var environmentModel = mapper.Map<Models.Environment>(requestedEnvironment);

            environmentModel.Id = currentEnvironment.Id;
            environmentModel.FeatureId = featureId;
            context.Entry(environmentModel).State = EntityState.Modified;
        }

        public async Task<Environment> Add(Environment entity, int featureId)
        {
            var model = mapper.Map<Models.Environment>(entity);
            model.FeatureId = featureId;

            dbSet.Add(model);
            await Save();

            return mapper.Map<Environment>(model);
        }
    }
}
