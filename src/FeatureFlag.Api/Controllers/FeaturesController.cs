using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FeatureFlag.Api.DbContexts;
using FeatureFlag.Api.Models;

namespace FeatureFlag.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly FeatureFlagContext _context;

        public FeaturesController(FeatureFlagContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Feature>>> GetFeatures()
        {
            return await _context.Features
                                 .Include(x => x.Environments)
                                 .ThenInclude(x => x.UserExceptions)
                                 .ToListAsync();
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Feature>> GetFeature(string name)
        {
            var feature = await _context.Features
                                        .Include(x => x.Environments)
                                        .ThenInclude(x => x.UserExceptions)
                                        .FirstOrDefaultAsync(x => x.Name == name);

            if (feature == null)
            {
                return NotFound();
            }

            return feature;
        }

        [HttpPost]
        public async Task<ActionResult<Feature>> PostFeature(Feature feature)
        {
            _context.Features.Add(feature);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFeature", new { id = feature.Id }, feature);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeature(int id, Feature feature)
        {
            if (id != feature.Id)
            {
                return BadRequest();
            }

            _context.Entry(feature).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeatureExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Feature>> DeleteFeature(int id)
        {
            var feature = await _context.Features.FindAsync(id);
            if (feature == null)
            {
                return NotFound();
            }

            _context.Features.Remove(feature);
            await _context.SaveChangesAsync();

            return feature;
        }

        private bool FeatureExists(int id)
        {
            return _context.Features.Any(e => e.Id == id);
        }
    }
}
