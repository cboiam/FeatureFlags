using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FeatureFlag.Application.Interfaces.AppServices;
using FeatureFlag.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace FeatureFlag.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IFeatureAppService featureAppService;

        public FeaturesController(IFeatureAppService featureAppService)
        {
            this.featureAppService = featureAppService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeatureResponse>>> GetFeatures(string environment,
                                                                                  [FromHeader]string userName)
        {
            var result = await featureAppService.GetAll(environment, userName);

            if (result == null || !result.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<FeatureResponse>> GetFeature(string name,
                                                                    string environment,
                                                                    [FromHeader]string userName)
        {
            var result = await featureAppService.Get(name, environment, userName);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<FeatureResponse>> PostFeature(FeaturePostRequest feature)
        {
            var result = await featureAppService.Add(feature);

            return CreatedAtAction("GetFeature", new { name = result.Name }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutFeature(int id, FeaturePutRequest feature)
        {
            await featureAppService.Update(id, feature);
            return Ok();
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult> DeleteFeature(string name, [Required]string environment)
        {
            var result = await featureAppService.Remove(name, environment);

            return result ? Ok() : StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}
