// ***********************************************************************
// Assembly         : DemoWebsite
// Author           : r.wienicke
// Created          : 08-01-2019
//
// Last Modified By : r.wienicke
// Last Modified On : 08-01-2019
// ***********************************************************************
// <copyright file="PaymentMethod.cs" company="connexion e.solutions">
//     Copyright (c) connexion e.solutions. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
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
        /// <value>The payload.</value>
        [JsonProperty(PropertyName = "payload")]
        public Payload Payload { get; set; }
        /// <summary>
        /// the name of the payment method
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}