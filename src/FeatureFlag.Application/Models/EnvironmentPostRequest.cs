using System.Collections.Generic;

namespace FeatureFlag.Application.Models
{
    public class EnvironmentPostRequest
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public IEnumerable<string> UsersEnabled { get; set; }
        public int FeatureId { get; set; }
    }
}
