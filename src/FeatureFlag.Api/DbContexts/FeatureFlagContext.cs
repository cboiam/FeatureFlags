using FeatureFlag.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FeatureFlag.Api.DbContexts
{
    public class FeatureFlagContext : DbContext
    {
        public FeatureFlagContext(DbContextOptions<FeatureFlagContext> options) : base(options) { }

        public DbSet<Feature> Features { get; set; }
    }
}
