using FeatureFlag.Domain.Models;
using System.Threading.Tasks;

namespace FeatureFlag.Application.Interfaces.Repositories
{
    public interface IFeatureRepository : IRepository<Feature>
    {
        Task<Feature> Get(string name);
    }
}
