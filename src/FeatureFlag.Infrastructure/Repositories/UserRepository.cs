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
    public class UserRepository : Repository<Models.User>, IUserRepository
    {
        public UserRepository(FeatureFlagContext context)
            : base(context) { }

        public async Task<bool> UpdateRange(IEnumerable<User> users, int environmentId)
        {
            var userModels = await dbSet.Where(u => u.EnvironmentId == environmentId).ToListAsync();

            var usersToRemove = userModels.Where(u => !users.Any(e => e.Name == u.Name));

            var usersToAdd = users.Where(u => !userModels.Any(e => e.Name == u.Name))
                                  .Select(u => new Models.User
                                  {
                                      Name = u.Name,
                                      EnvironmentId = environmentId
                                  });

            if (usersToRemove.Any())
            {
                dbSet.RemoveRange(usersToRemove);
            }

            if (usersToAdd.Any())
            {
                dbSet.AddRange(usersToAdd);
            }

            return await Save();
        }
    }
}
