// Copyright (c) 2019 connexion OG / Roman Wienicke
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DemoWebsite.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Currency
    {
        EUR, CHF, USD
    }
}