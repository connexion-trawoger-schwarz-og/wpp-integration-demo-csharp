// ***********************************************************************
// Assembly         : DemoWebsite
// Author           : r.wienicke
// Created          : 08-01-2019
//
// Last Modified By : r.wienicke
// Last Modified On : 08-01-2019
// ***********************************************************************
// <copyright file="WirecardConfiguration.cs" company="connexion e.solutions">
//     Copyright (c) connexion e.solutions. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;

namespace Wirecard.Models
{
    /// <summary>
    /// configuration for wirecard
    /// Implements the <see cref="System.Collections.Generic.HashSet{Wirecard.Models.WirecardEndpoint}" />
    /// </summary>
    /// <seealso cref="System.Collections.Generic.HashSet{Wirecard.Models.WirecardEndpoint}" />
    public class WirecardConfiguration : HashSet<WirecardEndpoint>
    {
        /// <summary>
        /// Gets the endpoint.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>WirecardEndpoint.</returns>
        public WirecardEndpoint GetEndpoint(string name)
        {
            return this.FirstOrDefault(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }



}