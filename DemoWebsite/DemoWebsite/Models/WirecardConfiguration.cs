// Copyright (c) 2019 connexion OG / Roman Wienicke
using DemoWebsite.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
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