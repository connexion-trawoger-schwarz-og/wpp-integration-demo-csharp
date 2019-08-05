// Copyright (c) 2019 connexion OG / Roman Wienicke
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace DemoWebsite.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RequestFormat
    {
        [EnumMember(Value = "application/json")]
        Json,
        [EnumMember(Value = "application/xml")]
        Xml,
        [EnumMember(Value = "application/json-signed")]
        JsonSigned,
        [EnumMember(Value = "application/x-www-form-urlencoded")]
        FormUrlEndocded
    }
}