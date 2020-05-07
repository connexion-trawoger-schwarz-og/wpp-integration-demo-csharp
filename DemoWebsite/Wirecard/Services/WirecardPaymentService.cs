// ***********************************************************************
// Assembly         : DemoWebsite
// Author           : r.wienicke
// Created          : 08-01-2019
//
// Last Modified By : r.wienicke
// Last Modified On : 08-05-2019
// ***********************************************************************
// <copyright file="WirecardPaymentService.cs" company="connexion e.solutions">
//     Copyright (c) connexion e.solutions. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Wirecard.Models;
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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System.Collections.Specialized;
using System.Net;
using Microsoft.Extensions.Logging;

namespace Wirecard.Services
{
    /// <summary>
    /// wirecard payment service
    /// </summary>
    public class WirecardPaymentService
    {
        private readonly ILogger<WirecardPaymentService> _logger;

        /// <summary>
        /// configuration
        /// </summary>
        private readonly WirecardConfiguration _wirecardConfiguration;

        /// <summary>
        /// context accessor
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly string _basePath;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="wirecardOptions">The wirecard options.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public WirecardPaymentService(IOptions<WirecardConfiguration> wirecardOptions, IHttpContextAccessor httpContextAccessor, ILogger<WirecardPaymentService> logger)
        {

            _logger = logger;
            _wirecardConfiguration = wirecardOptions.Value;
            _httpContextAccessor = httpContextAccessor;
        }


        /// <summary>Initializes a new instance of the <see cref="T:Wirecard.Services.WirecardPaymentService"/> class.</summary>
        /// <param name="pathToConfigFile">The path to configuration file.</param>
        /// <param name="basePath">The base path.</param>
        public WirecardPaymentService(string pathToConfigFile, string basePath)
        {

            var config = new ConfigurationBuilder()
                .AddJsonFile(pathToConfigFile, optional: false, reloadOnChange: true)
                .Build();
            _wirecardConfiguration = new WirecardConfiguration();
            config.GetSection("wirecard").Bind(_wirecardConfiguration);
            _basePath = basePath;
        }

        /// <summary>
        /// get the redirect url from wirecard
        /// </summary>
        /// <param name="paymentInfo">The payment information.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        /// <exception cref="Exception">Create redirect url failed: {await response.Content.ReadAsStringAsync()}{Environment.NewLine}</exception>
        public async Task<string> GetRedirectUrlFromWirecardAsync(PaymentInfo paymentInfo)
        {
            

            // set TLS Secuity
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
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
                return await CreateRedirectUrlJsonAsync(response);
            }
            else
            {
                return await CreateRedirectUrlXmlAsync(response);
            }
        }

        public string GetRedirectUrlFromWirecard(PaymentInfo paymentInfo)
        {
            // set TLS Secuity
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
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
            var response = client.PostAsync(endpoint.Uri, new StringContent(payload, Encoding.UTF8, contentType))
                .GetAwaiter().GetResult();

            // there was a problem and the request was not successfull
            if (!response.IsSuccessStatusCode)
            {
                // throw exception with responese data
                throw new Exception($"Create redirect url failed: {response.Content.ReadAsStringAsync().GetAwaiter().GetResult()}{Environment.NewLine}");
            }

            // extract redirect url from response
            if (paymentMethod.RequestType == RequestFormat.Json)
            {
                return CreateRedirectUrlJson(response);
            }
            else
            {
                return CreateRedirectUrlXml(response);
            }
        }

        /// <summary>
        /// create the payload for the rest request
        /// </summary>
        /// <param name="paymentInfo"><see cref="PaymentInfo" /></param>
        /// <param name="endpoint"><see cref="WirecardEndpoint" /></param>
        /// <param name="paymentMethod"><see cref="PaymentMethod" /></param>
        /// <returns>System.String.</returns>
        private string CreatePayload(PaymentInfo paymentInfo, WirecardEndpoint endpoint, WirecardPayment paymentMethod)
        {
            PaymentMethods paymentMethods = null;
            
            if (!endpoint.SelectOnWirecardPage)
            {
                paymentMethods = new PaymentMethods
                {
                    PaymentMethod = new PaymentMethod[] {
                            new PaymentMethod { Name = paymentInfo.TypeName ?? paymentMethod.Name }
                        }
                };
            }

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
                    PaymentMethods = paymentMethods,
                    SuccessRedirectUrl = GetRedirecturl(endpoint.SuccessRedirectUrl),
                    FailRedirectUrl = GetRedirecturl(endpoint.FailRedirectUrl),
                    CancelRedirectUrl = GetRedirecturl(endpoint.CancelRedirectUrl),
                    Descriptor = endpoint.Descriptor,
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
                    ),
                    Locale = paymentInfo.Locale
                }

            };
            // create json string form payload class
            var data = JsonConvert.SerializeObject(payload, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return data;

        }



        /// <summary>
        /// Gets the redirecturl.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>System.String.</returns>
        private string GetRedirecturl(string uri)
        {
            if (string.IsNullOrEmpty(uri))
                return null;
            return GetRedirecturl(new Uri(uri, UriKind.RelativeOrAbsolute));
        }

        // get the redirect url
        /// <summary>
        /// Gets the redirecturl.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>System.String.</returns>
        private string GetRedirecturl(Uri uri)
        {
            // if absolute / do nothing and return url
            if (uri.IsAbsoluteUri)
            {
                return uri.ToString();
            }

            // create an absolute url
            return string.Concat(_basePath?.TrimEnd('/') ??
                $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}", "/", uri.ToString().TrimStart('/'));
        }

        public PaymentResponse GetIpnPaymentResult(PaymentResponse response)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentResponse> GetIpnPaymentResultAsync(PaymentResponse response)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// create the basic auth header
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>AuthenticationHeaderValue.</returns>
        private AuthenticationHeaderValue CreateAuthenticationHeader(string username, string password)
        {
            return new AuthenticationHeaderValue(
            "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}")));
        }

        /// <summary>
        /// create the redirect url form response message
        /// </summary>
        /// <param name="httpResponseMessage">The HTTP response message.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        private async Task<string> CreateRedirectUrlJsonAsync(HttpResponseMessage httpResponseMessage)
        {
            var responseData = await httpResponseMessage.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(responseData);
            var redirect = json.Value<string>("payment-redirect-url");
            return redirect;
        }

        private string CreateRedirectUrlJson(HttpResponseMessage httpResponseMessage)
        {
            var responseData = httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            JObject json = JObject.Parse(responseData);
            var redirect = json.Value<string>("payment-redirect-url");
            return redirect;
        }

        /// <summary>
        /// create the redirect url form response message
        /// </summary>
        /// <param name="httpResponseMessage">The HTTP response message.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        private async Task<string> CreateRedirectUrlXmlAsync(HttpResponseMessage httpResponseMessage)
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

        private string CreateRedirectUrlXml(HttpResponseMessage httpResponseMessage)
        {
            var responseData = httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var responseItem = XDocument.Parse(responseData);
            XNamespace ns = "http://www.elastic-payments.com/schema/payment";

            var redirect = responseItem.Root
                .Element(ns + "payment-methods")
                .Element(ns + "payment-method")
                .Attribute("url").Value;

            return redirect;
        }

        public PaymentResponse GetPaymentResult(IFormCollection data)
        {
            var nameValueCollection = new NameValueCollection();

            foreach (var key in data.Keys)
            {
                nameValueCollection.Add(key, data[key]);
            }

            return GetPaymentResult(nameValueCollection, data["eppresponse"] == StringValues.Empty ? RequestFormat.Json : RequestFormat.Xml);
        }

        public PaymentResponse GetPaymentResult(NameValueCollection data)
        {
            return GetPaymentResult(data, data["eppresponse"] == StringValues.Empty ? RequestFormat.Json : RequestFormat.Xml);
        }


        /// <summary>Gets the payment result. 
        /// Not fully implemented...
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="format">The format.</param>
        /// <returns>PaymentResponse.</returns>
        public PaymentResponse GetPaymentResult(NameValueCollection data, RequestFormat format)
        {

            string responseBase64 = ""; //, signatureAlgorithm, responseBase64, type, custom_css_url, locale;

            switch (format)
            {
                case RequestFormat.Json:
                    {
                        //string signatureBase64 = data["response-signature-base64"];
                        //string signatureAlgorithm = data["response-signature-altorithm"];
                        responseBase64 = data["response-base64"];

                    }
                    break;
                case RequestFormat.Xml:
                    {
                        //string type = data["psp_name"];
                        //string custom_css_url = data["custom_css_url"];
                        //string locale = data["locale"];
                        responseBase64 = data["eppresponse"];
                    }
                    break;
                case RequestFormat.JsonSigned:
                    break;
                case RequestFormat.FormUrlEndocded:
                    break;
                default:
                    break;
            }

            return PaymentResponse.Parse(Base64Decode(responseBase64), format);
        }

        /// <summary>
        /// Base64s the encode.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <returns>System.String.</returns>
        public string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Base64s the decode.
        /// </summary>
        /// <param name="base64EncodedData">The base64 encoded data.</param>
        /// <returns>System.String.</returns>
        public string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }




    }


}
