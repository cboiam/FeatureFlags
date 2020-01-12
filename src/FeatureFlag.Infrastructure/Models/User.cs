using FeatureFlag.Infrastructure.Abstractions;

namespace FeatureFlag.Infrastructure.Models
{
    public class User : Model
    {
        public string Name { get; set; }
        public int EnvironmentId { get; set; }
    }
}
