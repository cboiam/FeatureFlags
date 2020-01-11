using FeatureFlag.Infrastructure.Abstractions;
using System.Collections.Generic;

namespace FeatureFlag.Infrastructure.Models
{
    public class Feature : Model
    {
        public string Name { get; set; }
        public List<Environment> Environments { get; set; }
    }
}
