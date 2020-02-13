using FeatureFlag.Application.Interfaces.AppServices;
using FeatureFlag.Application.Models;
using FeatureFlag.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeatureFlag.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class EnvironmentsController : ControllerBase
    {
        private readonly IEnvironmentAppService environmentAppService;

        public EnvironmentsController(IEnvironmentAppService environmentAppService)
        {
            this.environmentAppService = environmentAppService;
        }

        [HttpGet("features/{featureName}/environments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<Environment>>> GetAll(string featureName)
        {
            var result = await environmentAppService.GetAll(featureName);

            if (result == null || !result.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet("features/{featureName}/environments/{environmentName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Environment>> Get(string featureName, string environmentName)
        {
            var result = await environmentAppService.Get(featureName, environmentName);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("features/{featureName}/environments/{environmentName}/enabled")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Environment>> CheckEnabled(string featureName,
                                                                  string environmentName,
                                                                  [FromHeader]string userName)
        {
            var result = await environmentAppService.CheckEnabled(featureName, environmentName, userName);
            return Ok(new { Enabled = result });
        }

        [HttpPost("features/{featureId}/environments")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Environment>> Create(int featureId, EnvironmentPostRequest environment)
        {
            var result = await environmentAppService.Add(environment, featureId);
            return this.Created(result);
        }

        [HttpPut("features/{featureId}/environments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Update(int featureId, EnvironmentPutRequest environment)
        {
            var result = await environmentAppService.Update(environment, featureId);

            return result ? Ok() : this.InternalServerError();
        }

        [HttpPatch("features/{featureId}/environments/{id}/toggle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Toggle(int featureId, int id)
        {
            var result = await environmentAppService.Toggle(featureId, id);

            return result ? Ok() : this.InternalServerError();
        }

        [HttpDelete("features/{featureId}/environments/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete(int featureId, int id)
        {
            var result = await environmentAppService.Remove(featureId, id);

            return result ? Ok() : this.InternalServerError();
        }
    }
}