using FeatureFlag.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeatureFlag.Application.Interfaces.AppServices
{
    public interface IFeatureAppService
    {
        Task<IEnumerable<Feature>> GetAll(string currentEnvironment);
        Task<Feature> Get(string name, string currentEnvironment);
        Task<Feature> Add(Feature feature);
        Task<bool> Update(int id, Feature feature);
        Task<bool> Remove(int id);
    }
}
