using FeatureFlag.Infrastructure.DbContexts;
using FeatureFlag.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace FeatureFlag.Infrastructure.Repositories
{
    public class Repository<TModel> where TModel : Model
    {
        protected readonly FeatureFlagContext context;
        protected readonly DbSet<TModel> dbSet;

        public Repository(FeatureFlagContext context)
        {
            this.context = context;
            dbSet = context.Set<TModel>();
        }

        protected virtual async Task<TModel> Add(TModel model)
        {
            dbSet.Add(model);
            await Save();

            return model;
        }

        protected virtual async Task<bool> Remove(int id)
        {
            var model = await dbSet.FindAsync(id);
            
            if (model == null)
            {
                throw new MissingMemberException();
            }

            dbSet.Remove(model);

            return await Save();
        }

        protected virtual async Task<bool> Update(TModel model)
        {
            if (model.Id <= 0)
            {
                throw new ArgumentException("Invalid Id");
            }

            context.Entry(model).State = EntityState.Modified;

            try
            {
                await Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await Exists(model.Id))
                {
                    throw new MissingMemberException($"{typeof(TModel).Name} doesn't exist");
                }
                throw;
            }

            return true;
        }

        protected async Task<bool> Save() => await context.SaveChangesAsync() > 0;

        protected async Task<bool> Exists(int id) => await dbSet.AnyAsync(m => m.Id == id);
    }
}
