// Copyright (c) 2019 connexion OG / Roman Wienicke
using DemoWebsite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DemoWebsite.Services
{
    /// <summary>
    /// wirecard payment service
    /// </summary>
    public class WirecardPaymentService
    {
        /// <summary>
        /// configuration
        /// </summary>
        private readonly WirecardConfiguration _wirecardConfiguration;

        /// <summary>
        /// context accessor
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="wirecardOptions"></param>
        /// <param name="httpContextAccessor"></param>
        public WirecardPaymentService(IOptions<WirecardConfiguration> wirecardOptions, IHttpContextAccessor httpContextAccessor)
        {
            _wirecardConfiguration = wirecardOptions.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// get the redirect url from wirecard
        /// </summary>
        /// <param name="paymentInfo"></param>
        /// <returns></returns>
        public async Task<string> GetRedirectUrlFromWirecard(PaymentInfo paymentInfo)
        {
            // http client
            var client = new HttpClient();

            // get endpoint and payment method
            var endpoint = _wirecardConfiguration.GetEndpoint(paymentInfo.EndpointName);
            var paymentMethod = endpoint.GetPaymentMethod(paymentInfo.PaymentName);
            client.DefaultRequestHeaders.Authorization = CreateAuthenticationHeader(paymentMethod.Username, paymentMethod.Password);

            // set the request type
            var contentType = $"application/{paymentMethod.RequestType.ToString().ToLower()}";
            // create the payload for the wirecard rest request
            var payload = CreatePayload(paymentInfo, endpoint, paymentMethod);
            // get the response url from wirecard
            var response = await client.PostAsync(endpoint.Uri, new StringContent(payload, Encoding.UTF8, contentType));

            // there was a problem and the request was not successfull
            if (!response.IsSuccessStatusCode)
            {
                // throw exception with responese data
                throw new Exception($"Create redirect url failed: {await response.Content.ReadAsStringAsync()}{Environment.NewLine}");
            }

            // extract redirect url from response
            if (paymentMethod.RequestType == RequestFormat.Json)
            {
                return await CreateRedirectUrlJson(response);
            }
            else
            {
                return await CreateRedirectUrlXml(response);
            }
        }

        /// <summary>
        /// create the payload for the rest request
        /// </summary>
        /// <param name="paymentInfo"><see cref="PaymentInfo"/></param>
        /// <param name="endpoint"><see cref="WirecardEndpoint" /></param>
        /// <param name="paymentMethod"><see cref="PaymentMethod"/></param>
        /// <returns></returns>
        private string CreatePayload(PaymentInfo paymentInfo, WirecardEndpoint endpoint, WirecardPayment paymentMethod)
        {
            // create payload class
            var payload = new Payload
            {
                Payment = new Payment
                {
                    MerchantAccountId = new MerchantAccountId
                    {
                        Value = paymentMethod.MerchantAccountId
                    },
                    RequestId = paymentInfo.RequestId,
                    TransactionType = (paymentInfo.TransactionType ?? paymentMethod.DefaultTransactionType).ToString().ToLower(),
                    RequestedAmount = paymentInfo.RequestedAmount,
                    AccountHolder = paymentInfo.AccountHolder,
                    Shipping = paymentInfo.Shipping,
                    PaymentMethods = new PaymentMethods
                    {
                        PaymentMethod = new PaymentMethod[] {
                            new PaymentMethod { Name = paymentMethod.Name }
                        }
                    },
                    SuccessRedirectUrl = GetRedirecturl(endpoint.SuccessRedirectUrl),
                    FailRedirectUrl = GetRedirecturl(endpoint.FailRedirectUrl),
                    CancelRedirectUrl = GetRedirecturl(endpoint.CancelRedirectUrl),
                    Descriptor = "test",
                    Notifications = Notifications.Create(paymentMethod.RequestType, new Notification[] {
                            new Notification {
                                Url = GetRedirecturl(endpoint.IpnDefaultNotificationUrl)
                            },
                            new Notification {
                                Url = GetRedirecturl(endpoint.IpnSuccessNotificationUrl),
                                TransactionState = TransactionState.Success
                            },
                            new Notification {
                                Url = GetRedirecturl(endpoint.IpnFailedNotificationUrl),
                                TransactionState = TransactionState.Failed }
                            }
                    )
                }

            };
            // create json string form payload class
            var data = JsonConvert.SerializeObject(payload, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return data;

        }



        private string GetRedirecturl(string uri)
        {
            if (string.IsNullOrEmpty(uri))
                return null;
            return GetRedirecturl(new Uri(uri, UriKind.RelativeOrAbsolute));
        }

        // get the redirect url
        private string GetRedirecturl(Uri uri)
        {
            // if absolute / do nothing and return url
            if (uri.IsAbsoluteUri)
            {
                return uri.ToString();
            }

            // create an absolute url
            var request = _httpContextAccessor.HttpContext.Request;
            return string.Concat(request.Scheme, "://", request.Host, "/", uri.ToString().TrimStart('/'));
        }

        /// <summary>
        /// create the basic auth header
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private AuthenticationHeaderValue CreateAuthenticationHeader(string username, string password)
        {
            return new AuthenticationHeaderValue(
            "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}")));
        }

        /// <summary>
        /// create the redirect url form response message
        /// </summary>
        /// <param name="httpResponseMessage"></param>
        /// <returns></returns>
        private async Task<string> CreateRedirectUrlJson(HttpResponseMessage httpResponseMessage)
        {
            var responseData = await httpResponseMessage.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(responseData);
            var redirect = json.Value<string>("payment-redirect-url");
            return redirect;
        }

        /// <summary>
        /// create the redirect url form response message
        /// </summary>
        /// <param name="httpResponseMessage"></param>
        /// <returns></returns>
        private async Task<string> CreateRedirectUrlXml(HttpResponseMessage httpResponseMessage)
        {
            var responseData = await httpResponseMessage.Content.ReadAsStringAsync();
            var responseItem = XDocument.Parse(responseData);
            XNamespace ns = "http://www.elastic-payments.com/schema/payment";

            var redirect = responseItem.Root
                .Element(ns + "payment-methods")
                .Element(ns + "payment-method")
                .Attribute("url").Value;

            return redirect;
        }
        public string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }


}
