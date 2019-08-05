// Copyright (c) 2019 connexion OG / Roman Wienicke
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace DemoWebsite.Models
{
    public class Notifications
    {
        [JsonProperty(PropertyName = "format")]
        public RequestFormat Format { get; set; }

        [JsonProperty(PropertyName = "notification")]
        public HashSet<Notification> Notification { get; set; }

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