using FeatureFlag.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeatureFlag.Application.Interfaces.Repositories
{
    public interface IFeatureRepository : IRepository<Feature>
    {
        Task<IEnumerable<Feature>> GetAll(string currentEnvironment);
        Task<Feature> Get(string name, string currentEnvironment);
        Task<bool> Remove(string name, string currentEnvironment);
    }
}
