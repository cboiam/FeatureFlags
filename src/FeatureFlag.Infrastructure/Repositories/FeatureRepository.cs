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

        public FeatureRepository(FeatureFlagContext context,
                                 IMapper mapper,
                                 IEnvironmentRepository environmentRepository)
                                    : base(context, mapper)
        {
            this.environmentRepository = environmentRepository;
        }

        public async Task<Feature> Get(string name, string currentEnvironment)
        {
            var feature = await dbSet.AsNoTracking()
                .Include(f => f.Environments)
                .ThenInclude(e => e.UsersEnabled)
                .Select(f => new Models.Feature
                {
                    Id = f.Id,
                    Name = f.Name,
                    Environments = f.Environments.Where(e => e.Name == currentEnvironment).ToList()
                })
                .FirstOrDefaultAsync(f => f.Name == name);

            return mapper.Map<Feature>(feature);
        }

        public async Task<IEnumerable<Feature>> GetAll(string currentEnvironment)
        {
            var features = await dbSet.AsNoTracking()
                .Include(f => f.Environments)
                .ThenInclude(e => e.UsersEnabled)
                .Select(f => new Models.Feature
                {
                    Id = f.Id,
                    Name = f.Name,
                    Environments = f.Environments.Where(e => e.Name == currentEnvironment).ToList()
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

            var addedEnvironment = await environmentRepository.Add(environment);
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

            if(existingFeature == null)
            {
                throw new System.MissingMemberException("Feature doesn't exist");
            }

            var model = mapper.Map<Models.Feature>(entity);
            context.Entry(model).State = EntityState.Modified;
            
            if(existingFeature.Environments.Any())
            {
                var environmentModel = mapper.Map<Models.Environment>(environment);
                environmentModel.Id = existingFeature.Environments.First().Id;

                context.Entry(environmentModel).State = EntityState.Modified;

                var currentUsers = existingFeature.Environments.First().UsersEnabled;

                var usersToRemove = currentUsers.Where(u => !environment.UsersEnabled.Any(e => e.Name == u.Name));
                var usersToAdd = environment.UsersEnabled.Where(u => !currentUsers.Any(e => e.Name == u.Name))
                    .Select(u => new Models.User { Name = u.Name, EnvironmentId = environmentModel.Id });
                                
                if (usersToRemove.Any())
                {
                    context.Users.RemoveRange(mapper.Map<List<Models.User>>(usersToRemove));
                }

                if (usersToAdd.Any())
                {
                    context.Users.AddRange(usersToAdd);
                }

                model.Environments = new List<Models.Environment> { environmentModel };
            }

            var changes = await context.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<bool> Remove(int id, string currentEnvironment)
        {
            var feature = await Get(id);
            var environment = feature.Environments.FirstOrDefault(e => e.Name == currentEnvironment);

            return await environmentRepository.Remove(environment.Id);
        }
    }
}
