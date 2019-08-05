// ***********************************************************************
// Assembly         : DemoWebsite
// Author           : r.wienicke
// Created          : 08-02-2019
//
// Last Modified By : r.wienicke
// Last Modified On : 08-02-2019
// ***********************************************************************
// <copyright file="Shipping.cs" company="connexion e.solutions">
//     Copyright (c) connexion e.solutions. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace DemoWebsite.Models
{
    /// <summary>
    /// Class Shipping.
    /// </summary>
    public class Shipping
    {
        /// <summary>
        /// first name of account holder
        /// </summary>
        /// <value>The first name.</value>
        [XmlElement(ElementName = "first-name")]
        [JsonProperty(PropertyName = "first-name")]
        public string FirstName { get; set; }

        /// <summary>
        /// last name of account holder
        /// </summary>
        /// <value>The last name.</value>
        [XmlElement(ElementName = "last-name")]
        [JsonProperty(PropertyName = "last-name")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>The phone.</value>
        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        [JsonProperty(PropertyName = "address")]
        public Address Address { get; set; }
    }
}