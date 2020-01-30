using System.Collections.Generic;

namespace FeatureFlag.Application.Models
{
    public class EnvironmentPutRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public IEnumerable<UserRequest> UsersEnabled { get; set; }
    }
}
