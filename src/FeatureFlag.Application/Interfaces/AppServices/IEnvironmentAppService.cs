﻿using FeatureFlag.Application.Models;
using FeatureFlag.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeatureFlag.Application.Interfaces.AppServices
{
    public interface IEnvironmentAppService
    {
        Task<IEnumerable<Environment>> GetAll(string featureName);
        Task<Environment> Get(string featureName, string environmentName);
        Task<bool> CheckEnabled(string featureName, string environmentName, string userName);
        Task<Environment> Add(EnvironmentPostRequest environment);
        Task<bool> Update(EnvironmentPutRequest environment);
        Task<bool> Remove(int id);
        Task<bool> Toggle(int id);
    }
}