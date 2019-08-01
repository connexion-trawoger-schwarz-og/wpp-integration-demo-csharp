// Copyright (c) 2019 connexion OG / Roman Wienicke
using Newtonsoft.Json;

namespace DemoWebsite.Models
{
    /// <summary>
    /// wrapper class for statuses
    /// </summary>
    public class Statuses
    {
        [JsonProperty(PropertyName = "status")]
        public Status[] Status { get; set; }
    }
}