using FeatureFlag.Domain.Models;
using System.Threading.Tasks;

namespace FeatureFlag.Application.Interfaces.Repositories
{
    public interface IEnvironmentRepository : IRepository<Environment>
    {
        void Update(Environment currentEnvironment, Environment requestedEnvironment, int featureId);
        Task<Environment> Add(Environment entity, int featureId);
    }
}
