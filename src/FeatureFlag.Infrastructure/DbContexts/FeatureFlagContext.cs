using FeatureFlag.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FeatureFlag.Infrastructure.DbContexts
{
    public class FeatureFlagContext : DbContext
    {
        public FeatureFlagContext(DbContextOptions<FeatureFlagContext> options) : base(options) { }

        public DbSet<Feature> Features { get; set; }
        public DbSet<Environment> Environments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var feature = modelBuilder.Entity<Feature>();
            feature.HasIndex(f => f.Name);
            feature.Property(f => f.Name)
                .IsRequired();

            modelBuilder.Entity<Environment>()
                .Property(f => f.Name)
                .IsRequired();
        }
    }
}
