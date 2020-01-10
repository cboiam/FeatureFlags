using System.Collections.Generic;

namespace FeatureFlag.Api.Models
{
    public class Feature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Environment> Environments { get; set; }
    }
}
