using FeatureFlag.Application.Interfaces.AppServices;
using FeatureFlag.Application.Models;
using FeatureFlag.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public async Task<ActionResult<IEnumerable<Environment>>> GetAll(string featureName)
        {
            var result = await environmentAppService.GetAll(featureName);

            if(result == null || !result.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet("features/{featureName}/environments/{environmentName}")]
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
        public async Task<ActionResult<Environment>> CheckEnabled(string featureName, 
                                                                  string environmentName,
                                                                  [FromHeader]string userName)
        {
            var result = await environmentAppService.CheckEnabled(featureName, environmentName, userName);
            return Ok(new { Enabled = result });
        }

        [HttpPost("environments")]
        public async Task<ActionResult<Environment>> Create(EnvironmentPostRequest environment)
        {
            var result = await environmentAppService.Add(environment);

            var jsonResult = new JsonResult(result);
            jsonResult.StatusCode = (int)HttpStatusCode.Created;
            
            return jsonResult;
        }

        [HttpPut("environments")]
        public async Task<ActionResult> Update(EnvironmentPutRequest environment)
        {
            var result = await environmentAppService.Update(environment);

            return result ? Ok() : StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpPatch("environments/{id}/toggle")]
        public async Task<ActionResult> Toggle(int id)
        {
            var result = await environmentAppService.Toggle(id);

            return result ? Ok() : StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpDelete("environments/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await environmentAppService.Remove(id);

            return result ? Ok() : StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}