using AutoMapper;
using FeatureFlag.Application.Interfaces.AppServices;
using FeatureFlag.Application.Interfaces.Repositories;
using FeatureFlag.Application.Models;
using FeatureFlag.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeatureFlag.Application.AppServices
{
    public class FeatureAppService : IFeatureAppService
    {
        private readonly IFeatureRepository featureRepository;
        private readonly IMapper mapper;

        public FeatureAppService(IFeatureRepository featureRepository, IMapper mapper)
        {
            this.featureRepository = featureRepository;
            this.mapper = mapper;
        }

        public async Task<Feature> Add(FeaturePostRequest feature, string currentEnvironment)
        {
            var featureEntity = mapper.Map<Feature>(feature);
            featureEntity.Environments.First().Name = currentEnvironment;

            return await featureRepository.Add(featureEntity);
        }

        public async Task<Feature> Get(string name, string currentEnvironment)
        {
            return await featureRepository.Get(name, currentEnvironment);
        }

        public async Task<IEnumerable<Feature>> GetAll(string currentEnvironment)
        {
            return await featureRepository.GetAll(currentEnvironment);
        }

        public async Task<bool> Remove(int id, string currentEnvironment)
        {
            return await featureRepository.Remove(id, currentEnvironment);
        }

        public async Task<bool> Update(int id, FeaturePutRequest feature, string currentEnvironment)
        {
            var featureEntity = mapper.Map<Feature>(feature);
            featureEntity.Environments.First().Name = currentEnvironment;

            return await featureRepository.Update(id, featureEntity);
        }
    }
}
