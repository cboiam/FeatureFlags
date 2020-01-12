using FeatureFlag.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeatureFlag.Application.Interfaces.AppServices
{
    public interface IFeatureAppService
    {
        Task<IEnumerable<FeatureResponse>> GetAll(string currentEnvironment);
        Task<FeatureResponse> Get(string name, string currentEnvironment);
        Task<FeatureResponse> Add(FeaturePostRequest feature);
        Task<bool> Update(int id, FeaturePutRequest feature);
        Task<bool> Remove(string name, string currentEnvironment);
    }
}
