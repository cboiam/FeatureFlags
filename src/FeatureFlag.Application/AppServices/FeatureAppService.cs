using AutoMapper;
using FeatureFlag.Application.Interfaces.AppServices;
using FeatureFlag.Application.Interfaces.Repositories;
using FeatureFlag.Application.Models;
using FeatureFlag.Domain.Models;
using System.Collections.Generic;
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

        public async Task<Feature> Add(FeaturePostRequest feature)
        {
            var featureEntity = mapper.Map<Feature>(feature);

            return await featureRepository.Add(featureEntity);
        }

        public async Task<Feature> Get(string name)
        {
            return await featureRepository.Get(name);
        }

        public async Task<IEnumerable<Feature>> GetAll()
        {
            return await featureRepository.GetAll();
        }

        public async Task<bool> Remove(int id)
        {
            return await featureRepository.Remove(id);
        }

        public async Task<bool> Update(FeaturePutRequest feature)
        {
            var featureEntity = mapper.Map<Feature>(feature);

            return await featureRepository.Update(featureEntity);
        }
    }
}
