using FeatureFlag.Domain.Models;

namespace FeatureFlag.Application.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        void UpdateRange(Environment currentEnvironment, Environment requestedEnvironment);
    }
}
