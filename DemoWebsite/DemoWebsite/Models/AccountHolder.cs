// ***********************************************************************
// Assembly         : DemoWebsite
// Author           : r.wienicke
// Created          : 08-01-2019
//
// Last Modified By : r.wienicke
// Last Modified On : 08-05-2019
// ***********************************************************************
// <copyright file="AccountHolder.cs" company="connexion e.solutions">
//     Copyright (c) connexion e.solutions. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
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
        /// <value>The account information.</value>
        [XmlElement(ElementName = "account-info")]
        [JsonProperty(PropertyName = "account-info")]
        public AccountInfo AccountInfo { get; set; }

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
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

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

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>The date of birth.</value>
        [JsonProperty(PropertyName = "date-of-birth")]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>The gender.</value>
        [JsonProperty(PropertyName = "gender")]
        public Gender Gender { get; set; }

        /// <summary>
        /// Gets or sets the social security number.
        /// </summary>
        /// <value>The social security number.</value>
        [JsonProperty(PropertyName = "social-security-number")]
        public string SocialSecurityNumber { get; set; }
    }
}