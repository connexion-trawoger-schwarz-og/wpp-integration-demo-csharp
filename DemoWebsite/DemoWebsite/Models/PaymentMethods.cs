// ***********************************************************************
// Assembly         : DemoWebsite
// Author           : r.wienicke
// Created          : 08-01-2019
//
// Last Modified By : r.wienicke
// Last Modified On : 08-01-2019
// ***********************************************************************
// <copyright file="PaymentMethods.cs" company="connexion e.solutions">
//     Copyright (c) connexion e.solutions. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace DemoWebsite.Models
{
    /// <summary>
    /// payment method parent wrapper class
    /// </summary>
    public class PaymentMethods
    {
        /// <summary>
        /// the payment methods
        /// </summary>
        /// <value>The payment method.</value>
        [XmlElement(ElementName = "payment-method")]
        [JsonProperty(PropertyName = "payment-method")]
        public PaymentMethod[] PaymentMethod { get; set; }
    }
}