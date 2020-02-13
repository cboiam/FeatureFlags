using FeatureFlag.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeatureFlag.Application.Interfaces.Repositories
{
    public interface IEnvironmentRepository
    {
        Task<Environment> Get(string featureName, string environmentName);
        Task<IEnumerable<Environment>> GetAll(string featureName);
        Task<bool> Update(Environment entity, int featureId);
        Task<Environment> Add(Environment entity, int featureId);
        Task<bool> Toggle(int featureId, int id);
        Task<bool> Remove(int featureId, int id);
    }
}
