// Copyright (c) 2019 connexion OG / Roman Wienicke
using Newtonsoft.Json;
using System;

namespace DemoWebsite.Models
{
    public class Notification
    {
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "transaction-state")]
        public TransactionState? TransactionState { get; set; }
    }
}