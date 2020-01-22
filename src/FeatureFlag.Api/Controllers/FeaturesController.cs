using FeatureFlag.Application.Interfaces.AppServices;
using FeatureFlag.Application.Models;
using FeatureFlag.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FeatureFlag.Api.Controllers
{
    [Route("api/features")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IFeatureAppService featureAppService;

        public FeaturesController(IFeatureAppService featureAppService)
        {
            this.featureAppService = featureAppService;
        }

        [HttpGet]
        public async Task<ActionResult<Feature>> GetAll()
        {
            var result = await featureAppService.GetAll();

            if (result == null || !result.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Feature>> Get(string name)
        {
            var result = await featureAppService.Get(name);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Feature>> Create(FeaturePostRequest feature)
        {
            var result = await featureAppService.Add(feature);

            return CreatedAtAction("Get", new { name = result.Name }, result);
        }

        [HttpPut]
        public async Task<ActionResult> Update(FeaturePutRequest feature)
        {
            var result = await featureAppService.Update(feature);
            
            return result ? Ok() : StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await featureAppService.Remove(id);

            return result ? Ok() : StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}