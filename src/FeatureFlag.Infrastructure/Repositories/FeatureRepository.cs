using AutoMapper;
using FeatureFlag.Application.Interfaces.Repositories;
using FeatureFlag.Infrastructure.DbContexts;
using FeatureFlag.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace FeatureFlag.Infrastructure.Repositories
{
    public class FeatureRepository : Repository<Models.Feature, Feature>, IFeatureRepository
    {
        private readonly IEnvironmentRepository environmentRepository;
        private readonly IUserRepository userRepository;

        public FeatureRepository(FeatureFlagContext context,
                                 IMapper mapper,
                                 IEnvironmentRepository environmentRepository,
                                 IUserRepository userRepository)
                                    : base(context, mapper)
        {
            this.environmentRepository = environmentRepository;
            this.userRepository = userRepository;
        }

        public async Task<Feature> Get(string name, string environmentName)
        {
            var feature = await dbSet.AsNoTracking()
                .Include(f => f.Environments)
                .ThenInclude(e => e.UsersEnabled)
                .Select(f => new Models.Feature
                {
                    Id = f.Id,
                    Name = f.Name,
                    Environments = f.Environments.Where(e => string.IsNullOrEmpty(environmentName) ||
                                                        e.Name == environmentName)
                })
                .FirstOrDefaultAsync(f => f.Name == name);

            return mapper.Map<Feature>(feature);
        }

        public async Task<IEnumerable<Feature>> GetAll(string environmentName)
        {
            var features = await dbSet.AsNoTracking()
                .Include(f => f.Environments)
                .ThenInclude(e => e.UsersEnabled)
                .Select(f => new Models.Feature
                {
                    Id = f.Id,
                    Name = f.Name,
                    Environments = f.Environments.Where(e => string.IsNullOrEmpty(environmentName) || 
                                                             e.Name == environmentName)
                })
                .ToListAsync();

            return mapper.Map<List<Feature>>(features);
        }

        public override async Task<Feature> Add(Feature entity)
        {
            var environment = entity.Environments.First();
            var existingFeature = await Get(entity.Name, environment.Name);

            if (existingFeature == null)
            {
                return await base.Add(entity);
            }

            if (existingFeature.Environments.Any())
            {
                throw new System.InvalidOperationException("Environment already exists for this feature!");
            }

            var addedEnvironment = await environmentRepository.Add(environment, existingFeature.Id);
            existingFeature.Environments = new List<Environment> { addedEnvironment };

            return existingFeature;
        }

        public override async Task<bool> Update(int id, Feature entity)
        {
            if (id != entity.Id)
            {
                throw new System.ArgumentException("Ids should match!");
            }

            var environment = entity.Environments.First();
            var existingFeature = await Get(entity.Name, environment.Name);

            if (existingFeature == null)
            {
                throw new System.MissingMemberException("Feature doesn't exist");
            }

            var model = mapper.Map<Models.Feature>(entity);
            context.Entry(model).State = EntityState.Modified;

            if (existingFeature.Environments.Any())
            {
                var existingEnvironment = existingFeature.Environments.First();

                environmentRepository.Update(existingEnvironment, environment, existingFeature.Id);
                userRepository.UpdateRange(existingEnvironment, environment);
            }

            return await Save();
        }

        public async Task<bool> Remove(string name, string environmentName)
        {
            var feature = await dbSet.AsNoTracking()
                .Include(f => f.Environments)
                .ThenInclude(e => e.UsersEnabled)
                .FirstOrDefaultAsync(f => f.Name == name);

            if (feature == null)
            {
                throw new System.MissingMemberException("Feature doesn't exist");
            }

            var environment = feature.Environments.FirstOrDefault(e => e.Name == environmentName);

            if (environment != null && feature.Environments.Count() > 1)
            {
                return await environmentRepository.Remove(environment.Id);
            }

            if(environment != null)
            {
                return await Remove(feature.Id);
            }

            throw new System.MissingMemberException("Environment doesn't exist");
        }
    }
}
