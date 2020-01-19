using System.Collections.Generic;
using System.Linq;

namespace FeatureFlag.Domain.Models
{
    public class Feature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Environment> Environments { get; set; }
        public bool Enabled { get; private set; }

        public void SetIsEnabled(string currentEnvironment, string currentUser)
        {
            if (string.IsNullOrEmpty(currentEnvironment))
            {
                Enabled = Environments.All(e => e.Enabled);
                return;
            }

            var environment = Environments.FirstOrDefault(e => e.Name == currentEnvironment);

            Enabled = environment != null &&
                        (environment.Enabled ||
                         environment.UsersEnabled.Any(u => u.Name == currentUser));
        }
    }
}
