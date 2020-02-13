using FeatureFlag.Application.Interfaces.AppServices;
using FeatureFlag.Application.Models;
using FeatureFlag.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Feature>> Create(FeaturePostRequest feature)
        {
            var result = await featureAppService.Add(feature);
            return this.Created(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Update(FeaturePutRequest feature)
        {
            var result = await featureAppService.Update(feature);
            
            return result ? Ok() : this.InternalServerError();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await featureAppService.Remove(id);

            return result ? Ok() : this.InternalServerError();
        }
    }
}