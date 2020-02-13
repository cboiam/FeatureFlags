using FeatureFlag.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeatureFlag.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<bool> UpdateRange(IEnumerable<User> users, int environmentId);
    }
}
