// Copyright (c) 2019 connexion OG / Roman Wienicke
using Newtonsoft.Json;

namespace DemoWebsite.Models
{
    /// <summary>
    /// the requested amount
    /// </summary>
    public class RequestedAmount
    {
        /// <summary>
        /// the currency
        /// </summary>
        [JsonProperty(PropertyName = "currency")]
        public Currency Currency { get; set; }
        /// <summary>
        /// the value
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public decimal Value { get; set; }
    }
}