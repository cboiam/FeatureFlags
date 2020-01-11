using FeatureFlag.Application.Interfaces.AppServices;
using FeatureFlag.Application.Interfaces.Repositories;
using FeatureFlag.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeatureFlag.Application.AppServices
{
    public class FeatureAppService : IFeatureAppService
    {
        private readonly IFeatureRepository featureRepository;

        public FeatureAppService(IFeatureRepository featureRepository)
        {
            this.featureRepository = featureRepository;
        }

        public async Task<Feature> Add(Feature feature)
        {
            return await featureRepository.Add(feature);
        }

        public async Task<Feature> Get(string name, string currentEnvironment)
        {
            return await featureRepository.Get(name, currentEnvironment);
        }

        public async Task<IEnumerable<Feature>> GetAll(string currentEnvironment)
        {
            return await featureRepository.GetAll(currentEnvironment);
        }

        public async Task<bool> Remove(int id)
        {
            return await featureRepository.Remove(id);
        }

        public async Task<bool> Update(int id, Feature feature)
        {
            return await featureRepository.Update(id, feature);
        }
    }
}
