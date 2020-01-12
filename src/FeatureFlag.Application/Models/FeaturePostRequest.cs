using System.Collections.Generic;

namespace FeatureFlag.Application.Models
{
    public class FeaturePostRequest
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public string Environment { get; set; }
        public IEnumerable<string> EnabledUserNames { get; set; }
    }
}
