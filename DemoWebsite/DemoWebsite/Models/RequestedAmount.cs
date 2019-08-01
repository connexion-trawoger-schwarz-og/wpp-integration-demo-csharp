// Copyright (c) 2019 connexion OG / Roman Wienicke
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
        [JsonProperty(PropertyName = "currency"),
         JsonConverter(typeof(StringEnumConverter))]
        public Currency Currency { get; set; }
        /// <summary>
        /// the value
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public decimal Value { get; set; }
    }

    public enum Currency
    {
        EUR, CHF, USD
    }
}