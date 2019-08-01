// Copyright (c) 2019 connexion OG / Roman Wienicke
using DemoWebsite.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    public class JsonCreateTests
    {
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
                        LastName = "doe"
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
    }

       
}
