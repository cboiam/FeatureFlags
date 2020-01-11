using FeatureFlag.Infrastructure.Abstractions;
using System.Collections.Generic;

namespace FeatureFlag.Infrastructure.Models
{
    public class Environment : Model
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public List<User> UsersEnabled { get; set; }
    }
}
