using FeatureFlag.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeatureFlag.Application.Interfaces.AppServices
{
    public interface IFeatureAppService
    {
        Task<IEnumerable<FeatureResponse>> GetAll(string currentEnvironment, string currentUser);
        Task<FeatureResponse> Get(string name, string currentEnvironment, string currentUser);
        Task<FeatureResponse> Add(FeaturePostRequest feature);
        Task<bool> Update(int id, FeaturePutRequest feature);
        Task<bool> Remove(string name, string currentEnvironment);
    }
}
