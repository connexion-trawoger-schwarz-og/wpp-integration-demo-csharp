// ***********************************************************************
// Assembly         : DemoWebsite
// Author           : r.wienicke
// Created          : 08-01-2019
//
// Last Modified By : r.wienicke
// Last Modified On : 08-01-2019
// ***********************************************************************
// <copyright file="Payload.cs" company="connexion e.solutions">
//     Copyright (c) connexion e.solutions. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;

namespace Wirecard.Models
{
    /// <summary>
    /// the payload to be sent to wirecard
    /// </summary>
    public class Payload
    {
        /// <summary>
        /// Gets or sets the payment.
        /// </summary>
        /// <value>The payment.</value>
        [JsonProperty(PropertyName = "payment")]
        public Payment Payment { get; set; }
    }

}

