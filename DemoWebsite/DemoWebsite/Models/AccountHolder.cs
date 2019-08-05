// Copyright (c) 2019 connexion OG / Roman Wienicke
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
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

       

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "address")]
        public Address Address { get; set; }

        [JsonProperty(PropertyName = "date-of-birth")]
        public DateTime DateOfBirth { get; set; }

        [JsonProperty(PropertyName = "gender")]
        public Gender Gender { get; set; }

        [JsonProperty(PropertyName = "social-security-number")]
        public string SocialSecurityNumber { get; set; }
    }
}