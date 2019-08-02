// Copyright (c) 2019 connexion OG / Roman Wienicke
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace DemoWebsite.Models
{
    public class Shipping
    {
        /// <summary>
        /// first name of account holder
        /// </summary>
        [XmlElement(ElementName = "first-name")]
        [JsonProperty(PropertyName = "first-name")]
        public string FirstName { get; set; }

        /// <summary>
        /// last name of account holder
        /// </summary>
        [XmlElement(ElementName = "last-name")]
        [JsonProperty(PropertyName = "last-name")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "address")]
        public Address Address { get; set; }
    }
}