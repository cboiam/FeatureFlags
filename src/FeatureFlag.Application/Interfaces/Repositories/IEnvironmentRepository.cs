using FeatureFlag.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeatureFlag.Application.Interfaces.Repositories
{
    public interface IEnvironmentRepository : IRepository<Environment>
    {
        Task<Environment> Get(string featureName, string environmentName);
        Task<IEnumerable<Environment>> GetAll(string featureName);
        Task<Environment> Add(Environment entity, int featureId);
        Task<bool> Toggle(int id);
    }
}
