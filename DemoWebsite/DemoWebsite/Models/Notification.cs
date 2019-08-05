// ***********************************************************************
// Assembly         : DemoWebsite
// Author           : r.wienicke
// Created          : 08-05-2019
//
// Last Modified By : r.wienicke
// Last Modified On : 08-05-2019
// ***********************************************************************
// <copyright file="Notification.cs" company="connexion e.solutions">
//     Copyright (c) connexion e.solutions. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System;

namespace DemoWebsite.Models
{
    /// <summary>
    /// Class Notification.
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the state of the transaction.
        /// </summary>
        /// <value>The state of the transaction.</value>
        [JsonProperty(PropertyName = "transaction-state")]
        public TransactionState? TransactionState { get; set; }
    }
}