using FeatureFlag.Domain.Models;

namespace FeatureFlag.Application.Interfaces.Repositories
{
    public interface IEnvironmentRepository : IRepository<Environment>
    {
        void Update(Environment currentEnvironment, Environment requestedEnvironment);
    }
}
