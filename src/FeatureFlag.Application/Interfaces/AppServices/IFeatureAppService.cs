using FeatureFlag.Application.Models;
using FeatureFlag.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeatureFlag.Application.Interfaces.AppServices
{
    public interface IFeatureAppService
    {
        Task<IEnumerable<Feature>> GetAll();
        Task<Feature> Get(string name);
        Task<Feature> Add(FeaturePostRequest feature);
        Task<bool> Update(FeaturePutRequest feature);
        Task<bool> Remove(int id);
    }
}
