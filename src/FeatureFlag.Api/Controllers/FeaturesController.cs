using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FeatureFlag.Application.Interfaces.AppServices;
using FeatureFlag.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace FeatureFlag.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IFeatureAppService featureAppService;
        private readonly string currentEnvironment;

        public FeaturesController(IFeatureAppService featureAppService, IWebHostEnvironment environment)
        {
            this.featureAppService = featureAppService;
            currentEnvironment = environment.EnvironmentName;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Feature>>> GetFeatures()
        {
            var result = await featureAppService.GetAll(currentEnvironment);

            if (result == null || !result.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Feature>> GetFeature(string name)
        {
            var result = await featureAppService.Get(name, currentEnvironment);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Feature>> PostFeature(Feature feature)
        {
            var result = await featureAppService.Add(feature);

            return CreatedAtAction("GetFeature", new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeature(int id, Feature feature)
        {
            await featureAppService.Update(id, feature);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Feature>> DeleteFeature(int id)
        {
            var result = await featureAppService.Remove(id);

            return result ? Ok() : StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}
