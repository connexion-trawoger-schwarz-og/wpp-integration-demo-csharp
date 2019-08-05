// Copyright (c) 2019 connexion OG / Roman Wienicke
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace DemoWebsite.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TransactionState
    {
        Default,
        [EnumMember(Value = "success")]
        Success,
        [EnumMember(Value = "failed")]
        Failed
    }
}