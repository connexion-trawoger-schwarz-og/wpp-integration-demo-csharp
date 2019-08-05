// ***********************************************************************
// Assembly         : DemoWebsite
// Author           : r.wienicke
// Created          : 08-05-2019
//
// Last Modified By : r.wienicke
// Last Modified On : 08-05-2019
// ***********************************************************************
// <copyright file="TransactionState.cs" company="connexion e.solutions">
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
    /// Enum TransactionState
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TransactionState
    {
        /// <summary>
        /// The default
        /// </summary>
        Default,
        /// <summary>
        /// The success
        /// </summary>
        [EnumMember(Value = "success")]
        Success,
        /// <summary>
        /// The failed
        /// </summary>
        [EnumMember(Value = "failed")]
        Failed
    }
}