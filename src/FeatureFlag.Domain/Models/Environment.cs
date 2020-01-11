using System.Collections.Generic;

namespace FeatureFlag.Domain.Models
{
    public class Environment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public List<User> UsersEnabled { get; set; }
    }
}
