using AutoMapper;
using FeatureFlag.Application.Interfaces.Repositories;
using FeatureFlag.Infrastructure.DbContexts;
using FeatureFlag.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeatureFlag.Infrastructure.Repositories
{
    public class FeatureRepository : Repository<Models.Feature, Feature>, IFeatureRepository
    {
        public FeatureRepository(FeatureFlagContext context, IMapper mapper)
            : base(context, mapper) { }

        public async Task<Feature> Get(string name)
        {
            var feature = await dbSet.AsNoTracking()
                .Include(f => f.Environments)
                .ThenInclude(e => e.UsersEnabled)
                .FirstOrDefaultAsync(f => f.Name == name);

            return mapper.Map<Feature>(feature);
        }

        public override async Task<IEnumerable<Feature>> GetAll()
        {
            var features = await dbSet.AsNoTracking()
                .Include(f => f.Environments)
                .ThenInclude(e => e.UsersEnabled)
                .ToListAsync();

            return mapper.Map<List<Feature>>(features);
        }

        public override async Task<Feature> Add(Feature entity)
        {
            if (!await dbSet.AnyAsync(f => f.Name == entity.Name))
            {
                return await base.Add(entity);
            }
            throw new System.InvalidOperationException("Feature already exists!");
        }
    }
}
