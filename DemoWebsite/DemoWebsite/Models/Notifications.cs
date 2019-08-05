// Copyright (c) 2019 connexion OG / Roman Wienicke
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Runtime.Serialization;

namespace DemoWebsite.Models
{
    public class Notifications
    {
        [JsonProperty(PropertyName = "format"),
        JsonConverter(typeof(StringEnumConverter))]
        public RequestFormat Format { get; set; }

        [JsonProperty(PropertyName = "notification")]
        public Notification Notification { get; set; }
    }

    public class Notification
    {
        [JsonProperty(PropertyName = "url")]
        public Uri Url { get; set; }

        [JsonProperty(PropertyName = "transaction-state"),
        JsonConverter(typeof(StringEnumConverter))]
        public TransactionState TransactionState { get; set; }
    }

    public enum TransactionState
    {
        [EnumMember(Value = "success")]
        Success,
        [EnumMember(Value = "failed")]
        Failed
    }

    public enum RequestFormat
    {
        [EnumMember(Value = "application/json")]
        Json,
        [EnumMember(Value = "application/xml")]
        Xml
    }
}