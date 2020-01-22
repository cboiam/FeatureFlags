using System.Collections.Generic;

namespace FeatureFlag.Application.Models
{
    public class FeaturePostRequest
    {
        public string Name { get; set; }
        public IEnumerable<EnvironmentPostRequest> Environments { get; set; }
    }
}
