// Copyright (c) 2019 connexion OG / Roman Wienicke
using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoWebsite.Models
{
    /// <summary>
    /// configuration for wirecard
    /// </summary>
    public class WirecardConfiguration : HashSet<WirecardEndpoint>
    {
        public WirecardEndpoint GetEndpoint(string name)
        {
            return this.FirstOrDefault(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }



}