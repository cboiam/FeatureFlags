using AutoMapper;
using FeatureFlag.Application.Interfaces.AppServices;
using FeatureFlag.Application.Interfaces.Repositories;
using FeatureFlag.Application.Models;
using FeatureFlag.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeatureFlag.Application.AppServices
{
    public class EnvironmentAppService : IEnvironmentAppService
    {
        private readonly IEnvironmentRepository environmentRepository;
        private readonly IMapper mapper;

        public EnvironmentAppService(IEnvironmentRepository environmentRepository, IMapper mapper)
        {
            this.environmentRepository = environmentRepository;
            this.mapper = mapper;
        }

        public async Task<Environment> Add(EnvironmentPostRequest environment, int featureId)
        {
            var entity = mapper.Map<Environment>(environment);
            return await environmentRepository.Add(entity, featureId);
        }

        public async Task<bool> CheckEnabled(string featureName, string environmentName, string userName)
        {
            var environment = await Get(featureName, environmentName);

            if (environment == null)
            {
                throw new System.MissingMemberException("Environment doesn't exist");
            }

            return environment.CheckEnabled(userName);
        }

        public async Task<Environment> Get(string featureName, string environmentName)
        {
            return await environmentRepository.Get(featureName, environmentName);
        }

        public async Task<IEnumerable<Environment>> GetAll(string featureName)
        {
            return await environmentRepository.GetAll(featureName);
        }

        public async Task<bool> Remove(int id)
        {
            return await environmentRepository.Remove(id);
        }

        public async Task<bool> Toggle(int id)
        {
            return await environmentRepository.Toggle(id);
        }

        public async Task<bool> Update(EnvironmentPutRequest environment, int featureId)
        {
            var entity = mapper.Map<Environment>(environment);
            return await environmentRepository.Update(entity, featureId);
        }
    }
}
