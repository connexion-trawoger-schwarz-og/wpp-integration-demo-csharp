// Copyright (c) 2019 connexion OG / Roman Wienicke
using Newtonsoft.Json;

namespace DemoWebsite.Models
{
    public class Address
    {
        [JsonProperty(PropertyName = "street1")]
        public string Street1 { get; set; }

        [JsonProperty(PropertyName = "street2")]
        public string Street2 { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "postal-code")]
        public string PostalCode { get; set; }
    }
}