// ***********************************************************************
// Assembly         : DemoWebsite
// Author           : r.wienicke
// Created          : 08-05-2019
//
// Last Modified By : r.wienicke
// Last Modified On : 08-05-2019
// ***********************************************************************
// <copyright file="RequestFormat.cs" company="connexion e.solutions">
//     Copyright (c) connexion e.solutions. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Wirecard.Models
{
    /// <summary>
    /// Enum RequestFormat
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RequestFormat
    {
        /// <summary>
        /// The json
        /// </summary>
        [EnumMember(Value = "application/json")]
        Json,
        /// <summary>
        /// The XML
        /// </summary>
        [EnumMember(Value = "application/xml")]
        Xml,
        /// <summary>
        /// The json signed
        /// </summary>
        [EnumMember(Value = "application/json-signed")]
        JsonSigned,
        /// <summary>
        /// The form URL endocded
        /// </summary>
        [EnumMember(Value = "application/x-www-form-urlencoded")]
        FormUrlEndocded
    }
}