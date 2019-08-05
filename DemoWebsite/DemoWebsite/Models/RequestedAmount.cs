// ***********************************************************************
// Assembly         : DemoWebsite
// Author           : r.wienicke
// Created          : 08-01-2019
//
// Last Modified By : r.wienicke
// Last Modified On : 08-05-2019
// ***********************************************************************
// <copyright file="RequestedAmount.cs" company="connexion e.solutions">
//     Copyright (c) connexion e.solutions. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;

namespace DemoWebsite.Models
{
    /// <summary>
    /// the requested amount
    /// </summary>
    public class RequestedAmount
    {
        /// <summary>
        /// the currency
        /// </summary>
        /// <value>The currency.</value>
        [JsonProperty(PropertyName = "currency")]
        public Currency Currency { get; set; }
        /// <summary>
        /// the value
        /// </summary>
        /// <value>The value.</value>
        [JsonProperty(PropertyName = "value")]
        public decimal Value { get; set; }
    }
}