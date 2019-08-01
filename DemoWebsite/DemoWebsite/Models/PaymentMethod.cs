// Copyright (c) 2019 connexion OG / Roman Wienicke
using Newtonsoft.Json;

namespace DemoWebsite.Models
{
    /// <summary>
    /// the paymentmethod
    /// </summary>
    public class PaymentMethod
    {
        /// <summary>
        /// the payload
        /// </summary>
        [JsonProperty(PropertyName = "payload")]
        public Payload Payload { get; set; }
        /// <summary>
        /// the name of the payment method
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}