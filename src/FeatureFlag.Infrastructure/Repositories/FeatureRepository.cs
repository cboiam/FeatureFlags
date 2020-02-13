using AutoMapper;
using FeatureFlag.Application.Interfaces.Repositories;
using FeatureFlag.Infrastructure.DbContexts;
using FeatureFlag.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeatureFlag.Infrastructure.Repositories
{
    public class FeatureRepository : Repository<Models.Feature>, IFeatureRepository
    {
        private readonly IMapper mapper;

        public FeatureRepository(FeatureFlagContext context, IMapper mapper)
            : base(context)
        {
            this.mapper = mapper;
        }

        public async Task<Feature> Get(string name)
        {
            var feature = await dbSet.AsNoTracking()
                .Include(f => f.Environments)
                .ThenInclude(e => e.UsersEnabled)
                .FirstOrDefaultAsync(f => f.Name == name);

            return mapper.Map<Feature>(feature);
        }

        public async Task<IEnumerable<Feature>> GetAll()
        {
            var features = await dbSet.AsNoTracking()
                .Include(f => f.Environments)
                .ThenInclude(e => e.UsersEnabled)
                .ToListAsync();

            return mapper.Map<List<Feature>>(features);
        }

        public async Task<Feature> Add(Feature entity)
        {
            if (await dbSet.AnyAsync(f => f.Name == entity.Name))
            {
                throw new System.InvalidOperationException("Feature already exists!");
            }

            var model = mapper.Map<Models.Feature>(entity);
            model = await base.Add(model);

            return mapper.Map<Feature>(model);
        }

        public async Task<bool> Update(Feature entity)
        {
            var model = mapper.Map<Models.Feature>(entity);
            return await base.Update(model);
        }

        public async new Task<bool> Remove(int id) => await base.Remove(id);
    }
}
