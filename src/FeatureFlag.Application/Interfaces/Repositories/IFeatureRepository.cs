using FeatureFlag.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeatureFlag.Application.Interfaces.Repositories
{
    public interface IFeatureRepository
    {
        Task<IEnumerable<Feature>> GetAll();
        Task<Feature> Get(string name);
        Task<Feature> Add(Feature entity);
        Task<bool> Update(Feature entity);
        Task<bool> Remove(int id);
    }
}
