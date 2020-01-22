using System.Collections.Generic;
using System.Linq;

namespace FeatureFlag.Domain.Models
{
    public class Feature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Environment> Environments { get; set; }
    }
}
