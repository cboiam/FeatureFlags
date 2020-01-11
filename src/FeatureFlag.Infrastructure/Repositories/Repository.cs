using FeatureFlag.Application.Interfaces.Repositories;
using FeatureFlag.Infrastructure.DbContexts;
using FeatureFlag.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

namespace FeatureFlag.Infrastructure.Repositories
{
    public class Repository<TModel, TEntity> : IRepository<TEntity> where TModel : Model
    {
        protected readonly FeatureFlagContext context;
        protected readonly DbSet<TModel> dbSet;
        protected readonly IMapper mapper;

        public Repository(FeatureFlagContext context, IMapper mapper)
        {
            this.context = context;
            dbSet = context.Set<TModel>();
            this.mapper = mapper;
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            var model = mapper.Map<TModel>(entity);
            
            dbSet.Add(model);
            await Save();

            return mapper.Map<TEntity>(model);
        }

        public virtual async Task<TEntity> Get(int id)
        {
            var model = await dbSet.AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

            return mapper.Map<TEntity>(model);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            var models = await dbSet.AsNoTracking()
                .ToListAsync();

            return mapper.Map<List<TEntity>>(models);
        }

        public virtual async Task<bool> Remove(int id)
        {
            var model = await dbSet.FindAsync(id);
            
            if (model == null)
            {
                throw new MissingMemberException();
            }

            dbSet.Remove(model);

            return await Save();
        }

        public virtual async Task<bool> Update(int id, TEntity entity)
        {
            var model = mapper.Map<TModel>(entity);

            if (id != model.Id)
            {
                throw new ArgumentException();
            }

            context.Entry(model).State = EntityState.Modified;

            try
            {
                await Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await Exists(id))
                {
                    throw new MissingMemberException();
                }
                throw;
            }

            return true;
        }

        private async Task<bool> Save() => await context.SaveChangesAsync() > 0;

        private async Task<bool> Exists(int id) => await dbSet.AnyAsync(m => m.Id == id);
    }
}
