﻿using System.Collections.Generic;

namespace FeatureFlag.Application.Models
{
    public class FeaturePutRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public string Environment { get; set; }
        public IEnumerable<string> EnabledUserNames { get; set; }
    }
}
