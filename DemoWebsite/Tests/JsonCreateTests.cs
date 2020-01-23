// Copyright (c) 2019 connexion OG / Roman Wienicke

using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Wirecard.Models;

namespace Tests
{
    public class JsonCreateTests
    {
        [TestCase(RequestFormat.Json, @"""application/json""")]
        [TestCase(RequestFormat.Xml, @"""application/xml""")]
        public void EnumParseTest(RequestFormat format, string expected)
        {
            string parsedFormat = JsonConvert.SerializeObject(format);

            Assert.AreEqual(parsedFormat, expected);
        }

        [TestCase()]
        public void CreateJsonTest()
        {
            var payload = new Payload
            {
                Payment = new Payment
                {
                    MerchantAccountId = new MerchantAccountId
                    {
                        Value = "7a6dd74f-06ab-4f3f-a864-adc52687270a"
                    },
                    RequestId = "a63b8b80-cb8f-4fef-b1b3-e8db2dc26cc8",
                    TransactionType = "authorization",
                    RequestedAmount = new RequestedAmount
                    {
                        Value = 12.3m,
                        Currency = Currency.EUR
                    },
                    AccountHolder = new AccountHolder
                    {
                        FirstName = "john",
                        LastName = "doe",
                        Gender = Gender.M
                    },
                    PaymentMethods = new PaymentMethods
                    {
                        PaymentMethod = new PaymentMethod[] {
                            new PaymentMethod { Name = "creditcard" }
                        }
                    },
                    SuccessRedirectUrl = "/Success",
                    FailRedirectUrl = "/Fail",
                    CancelRedirectUrl = "/Cancel",
                    Descriptor = "test"
                }

            };


            var x = JsonConvert.SerializeObject(payload, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

        }


        [TestCase()]
        public void EnumConvertTests()
        {
            var actual = JsonConvert.SerializeObject(ShippingMethod.OtherAddressDelivery);
            var expected = "\"other_address_delivery\"";
            Assert.AreEqual(actual, expected);

            var shipping = new Shipping {
                ShippingMethod = ShippingMethod.DigitalGoods
            };

            var test = JsonConvert.SerializeObject(shipping);
        }

        [TestCase("cnxtest-1ccc9cd7-b6d4-4b39-b638-f072424f5ff9-check-payer-response.txt")]
        public void IPNStatus(string requestId)
        {
            var test = requestId.Substring(0, 44);
        }

    }
}
