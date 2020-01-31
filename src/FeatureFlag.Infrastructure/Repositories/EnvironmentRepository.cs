using AutoMapper;
using FeatureFlag.Application.Interfaces.Repositories;
using FeatureFlag.Domain.Models;
using FeatureFlag.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeatureFlag.Infrastructure.Repositories
{
    public class EnvironmentRepository : Repository<Models.Environment, Environment>, IEnvironmentRepository
    {
        private readonly IUserRepository userRepository;

        public EnvironmentRepository(FeatureFlagContext context, IMapper mapper, IUserRepository userRepository)
            : base(context, mapper)
        {
            this.userRepository = userRepository;
        }

        public async Task<Environment> Get(string featureName, string environmentName)
        {
            var environment = await dbSet.AsNoTracking()
                .Include(e => e.UsersEnabled)
                .FirstOrDefaultAsync(e => e.Feature.Name == featureName && e.Name == environmentName);

            return mapper.Map<Environment>(environment);
        }

        public async Task<IEnumerable<Environment>> GetAll(string featureName)
        {
            var environment = await dbSet.AsNoTracking()
                .Include(e => e.UsersEnabled)
                .Where(e => e.Feature.Name == featureName)
                .ToListAsync();

            return mapper.Map<List<Environment>>(environment);
        }

        public override async Task<bool> Update(Environment entity)
        {
            var environment = await base.Update(entity);
            var users = await userRepository.UpdateRange(entity.UsersEnabled, entity.Id);

            return environment || users;
        }

        public async Task<Environment> Add(Environment entity, int featureId)
        {
            if (dbSet.Any(e => e.FeatureId == featureId && e.Name == entity.Name))
            {
                throw new System.InvalidOperationException("Environment already exist for this feature");
            }

            var model = mapper.Map<Models.Environment>(entity);
            model.FeatureId = featureId;

            dbSet.Add(model);
            await Save();

            return mapper.Map<Environment>(model);
        }

        public async Task<bool> Toggle(int id)
        {
            var model = await dbSet.FirstOrDefaultAsync(e => e.Id == id);

            model.Enabled = !model.Enabled;

            context.Entry(model).State = EntityState.Modified;
            return await Save();
        }
    }
}
