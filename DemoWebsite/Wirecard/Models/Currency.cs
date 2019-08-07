// ***********************************************************************
// Assembly         : DemoWebsite
// Author           : r.wienicke
// Created          : 08-05-2019
//
// Last Modified By : r.wienicke
// Last Modified On : 08-05-2019
// ***********************************************************************
// <copyright file="Currency.cs" company="connexion e.solutions">
//     Copyright (c) connexion e.solutions. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wirecard.Models
{
    /// <summary>
    /// Enum Currency
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Currency
    {
        /// <summary>
        /// The eur
        /// </summary>
        EUR, CHF, USD
    }
}