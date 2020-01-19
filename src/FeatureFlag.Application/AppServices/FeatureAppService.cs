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

        public async Task<FeatureResponse> Add(FeaturePostRequest feature)
        {
            var featureEntity = mapper.Map<Feature>(feature);

            var addedFeature = await featureRepository.Add(featureEntity);
            addedFeature.SetIsEnabled(feature.Environment, null);

            return mapper.Map<FeatureResponse>(addedFeature);
        }

        public async Task<FeatureResponse> Get(string name, string currentEnvironment, string currentUser)
        {
            var feature = await featureRepository.Get(name, currentEnvironment);
            feature.SetIsEnabled(currentEnvironment, currentUser);

            return mapper.Map<FeatureResponse>(feature);
        }

        public async Task<IEnumerable<FeatureResponse>> GetAll(string currentEnvironment, string currentUser)
        {
            var features = await featureRepository.GetAll(currentEnvironment);
            features.ToList().ForEach(f => f.SetIsEnabled(currentEnvironment, currentUser));

            return mapper.Map<List<FeatureResponse>>(features);
        }

        public async Task<bool> Remove(string name, string currentEnvironment)
        {
            return await featureRepository.Remove(name, currentEnvironment);
        }

        public async Task<bool> Update(int id, FeaturePutRequest feature)
        {
            var featureEntity = mapper.Map<Feature>(feature);

            return await featureRepository.Update(id, featureEntity);
        }
    }
}
