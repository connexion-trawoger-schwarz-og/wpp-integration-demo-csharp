// Copyright (c) 2019 connexion OG / Roman Wienicke

using System.Runtime.Serialization;

namespace DemoWebsite.Models
{
    public enum Gender
    {
        [EnumMember(Value = "m")]
        M,
        [EnumMember(Value = "f")]
        F
    }
}