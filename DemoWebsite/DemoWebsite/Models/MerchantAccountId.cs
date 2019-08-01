// Copyright (c) 2019 connexion OG / Roman Wienicke
using Newtonsoft.Json;

namespace DemoWebsite.Models
{
    /// <summary>
    /// the wirecard merchant id
    /// </summary>
    public class MerchantAccountId
    {
        /// <summary>
        /// the value of the id
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
    }
}