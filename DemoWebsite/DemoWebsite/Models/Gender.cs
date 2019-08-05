﻿// Copyright (c) 2019 connexion OG / Roman Wienicke

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace DemoWebsite.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Gender
    {
        [EnumMember(Value = "m")]
        M,
        [EnumMember(Value = "f")]
        F
    }
}