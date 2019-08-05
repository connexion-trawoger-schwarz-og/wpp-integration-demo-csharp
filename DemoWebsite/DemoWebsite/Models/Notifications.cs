// ***********************************************************************
// Assembly         : DemoWebsite
// Author           : r.wienicke
// Created          : 08-05-2019
//
// Last Modified By : r.wienicke
// Last Modified On : 08-05-2019
// ***********************************************************************
// <copyright file="Notifications.cs" company="connexion e.solutions">
//     Copyright (c) connexion e.solutions. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace DemoWebsite.Models
{
    /// <summary>
    /// Class Notifications.
    /// </summary>
    public class Notifications
    {
        /// <summary>
        /// Gets or sets the format.
        /// </summary>
        /// <value>The format.</value>
        [JsonProperty(PropertyName = "format")]
        public RequestFormat Format { get; set; }

        /// <summary>
        /// Gets or sets the notification.
        /// </summary>
        /// <value>The notification.</value>
        [JsonProperty(PropertyName = "notification")]
        public HashSet<Notification> Notification { get; set; }

        /// <summary>
        /// Creates the specified format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="notifications">The notifications.</param>
        /// <returns>Notifications.</returns>
        public static Notifications Create(RequestFormat format, Notification[] notifications)
        {
            var items = notifications.Where(n => n.Url != null);
            if (items.Count() == 0)
            {
                return null;
            }

            return new Notifications {
                Format = format,
                Notification = new HashSet<Notification>(items)
            };
        }
    }
}