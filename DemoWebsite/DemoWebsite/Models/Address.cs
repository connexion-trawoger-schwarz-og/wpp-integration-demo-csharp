// ***********************************************************************
// Assembly         : DemoWebsite
// Author           : r.wienicke
// Created          : 08-02-2019
//
// Last Modified By : r.wienicke
// Last Modified On : 08-02-2019
// ***********************************************************************
// <copyright file="Address.cs" company="connexion e.solutions">
//     Copyright (c) connexion e.solutions. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;

namespace DemoWebsite.Models
{
    /// <summary>
    /// Class Address.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets the street1.
        /// </summary>
        /// <value>The street1.</value>
        [JsonProperty(PropertyName = "street1")]
        public string Street1 { get; set; }

        /// <summary>
        /// Gets or sets the street2.
        /// </summary>
        /// <value>The street2.</value>
        [JsonProperty(PropertyName = "street2")]
        public string Street2 { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>The country.</value>
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>The postal code.</value>
        [JsonProperty(PropertyName = "postal-code")]
        public string PostalCode { get; set; }
    }
}