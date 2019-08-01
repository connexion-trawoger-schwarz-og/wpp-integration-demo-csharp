// Copyright (c) 2019 connexion OG / Roman Wienicke
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace DemoWebsite.Models
{
    /// <summary>
    /// account holder
    /// </summary>
    public class AccountHolder
    {
        /// <summary>
        /// account info (empty at the moment)
        /// </summary>
        [XmlElement(ElementName = "account-info")]
        [JsonProperty(PropertyName = "account-info")]
        public AccountInfo AccountInfo { get; set; }

        /// <summary>
        /// last name of account holder
        /// </summary>
        [XmlElement(ElementName = "last-name")]
        [JsonProperty(PropertyName = "last-name")]
        public string LastName { get; set; }

        /// <summary>
        /// first name of account holder
        /// </summary>
        [XmlElement(ElementName = "first-name")]
        [JsonProperty(PropertyName = "first-name")]
        public string FirstName { get; set; }
    }
}