using FeatureFlag.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeatureFlag.Application.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> UpdateRange(IEnumerable<User> users, int environmentId);
    }
}
