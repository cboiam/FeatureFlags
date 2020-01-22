using AutoMapper;
using FeatureFlag.Application.Interfaces.Repositories;
using FeatureFlag.Domain.Models;
using FeatureFlag.Infrastructure.DbContexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeatureFlag.Infrastructure.Repositories
{
    public class UserRepository : Repository<Models.User, User>, IUserRepository
    {
        public UserRepository(FeatureFlagContext context, IMapper mapper) 
            : base(context, mapper) { }

        public async Task<bool> UpdateRange(IEnumerable<User> users, int environmentId)
        {
            dbSet.RemoveRange();
            var usersToRemove = dbSet.Where(u => u.EnvironmentId == environmentId && !users.Any(e => e.Name == u.Name));
            var usersToAdd = dbSet.Where(u => u.EnvironmentId == environmentId && !users.Any(e => e.Name == u.Name));

            if (usersToRemove.Any())
            {
                context.Users.RemoveRange(usersToRemove);
            }

            if (usersToAdd.Any())
            {
                context.Users.AddRange(usersToAdd);
            }

            return await Save();
        }
    }
}
