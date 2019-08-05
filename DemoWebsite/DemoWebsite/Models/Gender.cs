// ***********************************************************************
// Assembly         : DemoWebsite
// Author           : r.wienicke
// Created          : 08-02-2019
//
// Last Modified By : r.wienicke
// Last Modified On : 08-05-2019
// ***********************************************************************
// <copyright file="Gender.cs" company="connexion e.solutions">
//     Copyright (c) connexion e.solutions. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace DemoWebsite.Models
{
    /// <summary>
    /// Enum Gender
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Gender
    {
        /// <summary>
        /// The m
        /// </summary>
        [EnumMember(Value = "m")]
        M,
        /// <summary>
        /// The f
        /// </summary>
        [EnumMember(Value = "f")]
        F
    }
}