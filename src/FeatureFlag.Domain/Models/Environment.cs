using System;
using System.Collections.Generic;
using System.Linq;

namespace FeatureFlag.Domain.Models
{
    public class Environment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public IEnumerable<User> UsersEnabled { get; set; }

        public bool CheckEnabled(string userName) => Enabled || 
                                                     UsersEnabled.Any(u => u.Name == userName);
    }
}
