using AutoMapper;
using FeatureFlag.Application.Interfaces.Repositories;
using FeatureFlag.Domain.Models;
using FeatureFlag.Infrastructure.DbContexts;
using System.Collections.Generic;
using System.Linq;

namespace FeatureFlag.Infrastructure.Repositories
{
    public class UserRepository : Repository<Models.User, User>, IUserRepository
    {
        public UserRepository(FeatureFlagContext context, IMapper mapper) 
            : base(context, mapper) { }

        public void UpdateRange(Environment currentEnvironment, Environment requestedEnvironment)
        {
            var currentUsers = currentEnvironment.UsersEnabled;
            var requestedUsers = requestedEnvironment.UsersEnabled;

            var usersToRemove = currentUsers.Where(u => !requestedUsers.Any(e => e.Name == u.Name));
            var usersToAdd = requestedUsers.Where(u => !currentUsers.Any(e => e.Name == u.Name))
                .Select(u => new Models.User { Name = u.Name, EnvironmentId = currentEnvironment.Id });

            if (usersToRemove.Any())
            {
                context.Users.RemoveRange(mapper.Map<List<Models.User>>(usersToRemove));
            }

            if (usersToAdd.Any())
            {
                context.Users.AddRange(usersToAdd);
            }
        }
    }
}
