// ***********************************************************************
// Assembly         : DemoWebsite
// Author           : r.wienicke
// Created          : 08-01-2019
//
// Last Modified By : r.wienicke
// Last Modified On : 08-01-2019
// ***********************************************************************
// <copyright file="MerchantAccountId.cs" company="connexion e.solutions">
//     Copyright (c) connexion e.solutions. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;

namespace DemoWebsite.Models
{
    /// <summary>
    /// the wirecard merchant id
    /// </summary>
    public class MerchantAccountId
    {
        /// <summary>
        /// the value of the id
        /// </summary>
        /// <value>The value.</value>
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
    }
}