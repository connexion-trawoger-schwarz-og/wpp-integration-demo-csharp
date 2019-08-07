// ***********************************************************************
// Assembly         : DemoWebsite
// Author           : r.wienicke
// Created          : 08-01-2019
//
// Last Modified By : r.wienicke
// Last Modified On : 08-01-2019
// ***********************************************************************
// <copyright file="Device.cs" company="connexion e.solutions">
//     Copyright (c) connexion e.solutions. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Wirecard.Models
{
    /// <summary>
    /// customers device info
    /// </summary>
    public class Device
    {
        /// <summary>
        /// fingerprint value of device
        /// </summary>
        /// <value>The fingerprint.</value>
        public string Fingerprint { get; set; }
    }
}