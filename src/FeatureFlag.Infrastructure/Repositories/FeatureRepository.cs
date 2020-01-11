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
        public FeatureRepository(FeatureFlagContext context, IMapper mapper)
            : base (context, mapper) { }

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
    }
}
