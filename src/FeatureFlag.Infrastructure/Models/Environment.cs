using FeatureFlag.Infrastructure.Abstractions;
using System.Collections.Generic;

namespace FeatureFlag.Infrastructure.Models
{
    public class Environment : Model
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public IEnumerable<User> UsersEnabled { get; set; }
        public int FeatureId { get; set; }
    }
}
