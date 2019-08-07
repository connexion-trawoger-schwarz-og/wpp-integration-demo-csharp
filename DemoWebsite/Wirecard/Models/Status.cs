// ***********************************************************************
// Assembly         : DemoWebsite
// Author           : r.wienicke
// Created          : 08-01-2019
//
// Last Modified By : r.wienicke
// Last Modified On : 08-01-2019
// ***********************************************************************
// <copyright file="Status.cs" company="connexion e.solutions">
//     Copyright (c) connexion e.solutions. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;

namespace Wirecard.Models
{
    /// <summary>
    /// payment status
    /// </summary>
    public class Status
    {
        /// <summary>
        /// description
        /// </summary>
        /// <value>The description.</value>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        /// <summary>
        /// severity
        /// </summary>
        /// <value>The severity.</value>
        [JsonProperty(PropertyName = "severity")]
        public string Severity { get; set; }
        /// <summary>
        /// code
        /// </summary>
        /// <value>The code.</value>
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }
    }
}