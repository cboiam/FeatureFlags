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
    public class EnvironmentRepository : Repository<Models.Environment>, IEnvironmentRepository
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public EnvironmentRepository(FeatureFlagContext context, IMapper mapper, IUserRepository userRepository)
            : base(context)
        {
            this.mapper = mapper;
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

        public async Task<bool> Update(Environment entity, int featureId)
        {
            await ValidateEnvironmentName(entity, featureId);

            var model = mapper.Map<Models.Environment>(entity);
            model.FeatureId = featureId;
            
            var environment = await base.Update(model);
            var users = await userRepository.UpdateRange(entity.UsersEnabled, entity.Id);

            return environment || users;
        }

        public async Task<Environment> Add(Environment entity, int featureId)
        {
            await ValidateEnvironmentName(entity, featureId);

            var model = mapper.Map<Models.Environment>(entity);
            model.FeatureId = featureId;

            dbSet.Add(model);
            await Save();

            return mapper.Map<Environment>(model);
        }

        public async Task<bool> Toggle(int featureId, int id)
        {
            var model = await dbSet.FirstOrDefaultAsync(e => e.FeatureId == featureId && e.Id == id);

            model.Enabled = !model.Enabled;

            context.Entry(model).State = EntityState.Modified;
            return await Save();
        }

        public async Task<bool> Remove(int featureId, int id)
        {
            var model = await dbSet.FirstOrDefaultAsync(e => e.FeatureId == featureId && e.Id == id);
            dbSet.Remove(model);

            return await Save();
        }

        private async Task ValidateEnvironmentName(Environment environment, int featureId)
        {
            if (await dbSet.AnyAsync(e => e.FeatureId == featureId &&
                                          e.Id != environment.Id &&
                                          e.Name == environment.Name))
            {
                throw new System.InvalidOperationException("Environment already exist for this feature");
            }
        }
    }
}
