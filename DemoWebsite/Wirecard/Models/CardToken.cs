// ***********************************************************************
// Assembly         : DemoWebsite
// Author           : r.wienicke
// Created          : 08-01-2019
//
// Last Modified By : r.wienicke
// Last Modified On : 08-01-2019
// ***********************************************************************
// <copyright file="CardToken.cs" company="connexion e.solutions">
//     Copyright (c) connexion e.solutions. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Wirecard.Models
{
    /// <summary>
    /// card token used for accessing the same card without need of real number
    /// </summary>
    public class CardToken
    {
        /// <summary>
        /// the token id to be sent with the request to access the stored card
        /// </summary>
        /// <value>The token identifier.</value>
        [XmlElement(ElementName = "token-id")]
        [JsonProperty(PropertyName = "token-id")]
        public string TokenId { get; set; }

        /// <summary>
        /// the masked card number
        /// </summary>
        /// <value>The masked account number.</value>
        [XmlElement(ElementName = "masked-account-number")]
        [JsonProperty(PropertyName = "masked-account-number")]
        public string MaskedAccountNumber { get; set; }
    }
}