// ***********************************************************************
// Assembly         : DemoWebsite
// Author           : r.wienicke
// Created          : 08-02-2019
//
// Last Modified By : r.wienicke
// Last Modified On : 08-02-2019
// ***********************************************************************
// <copyright file="Shipping.cs" company="connexion e.solutions">
//     Copyright (c) connexion e.solutions. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Wirecard.Models
{
    /// <summary>
    /// Enum ShippingMethod
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ShippingMethod 
    {
        [EnumMember(Value = "other_verified")]
        OtherVerified,
        [EnumMember(Value = "home_delivery")]
        HomeDelivery,
        [EnumMember(Value = "verified_address_delivery")]
        VerifiedAddressDelivery,
        [EnumMember(Value = "other_address_delivery")]
        OtherAddressDelivery,
        [EnumMember(Value = "store_pick_up")]
        StorePickUp,
        [EnumMember(Value = "digital_goods")]
        DigitalGoods,
        [EnumMember(Value = "digital_tickets")]
        DigitalTickets
    }

}