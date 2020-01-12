using FeatureFlag.Application.Models;
using FeatureFlag.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeatureFlag.Application.Interfaces.AppServices
{
    public interface IFeatureAppService
    {
        Task<IEnumerable<Feature>> GetAll(string currentEnvironment);
        Task<Feature> Get(string name, string currentEnvironment);
        Task<Feature> Add(FeaturePostRequest feature, string currentEnvironment);
        Task<bool> Update(int id, FeaturePutRequest feature, string currentEnvironment);
        Task<bool> Remove(int id, string currentEnvironment);
    }
}
