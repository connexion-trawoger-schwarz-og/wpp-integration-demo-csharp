// ***********************************************************************
// Assembly         : DemoWebsite
// Author           : r.wienicke
// Created          : 07-31-2019
//
// Last Modified By : r.wienicke
// Last Modified On : 08-05-2019
// ***********************************************************************
// <copyright file="HomeController.cs" company="connexion e.solutions">
//     Copyright (c) connexion e.solutions. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
//using DemoWebsite.Models;
//using DemoWebsite.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Wirecard.Models;
using Wirecard.Services;

namespace DemoWebsite.Controllers
{
    /// <summary>
    /// startup controller
    /// Implements the <see cref="Microsoft.AspNetCore.Mvc.Controller" />
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class HomeController : Controller
    {
        /// <summary>
        /// injected wirecard payment service
        /// </summary>
        private readonly WirecardPaymentService _wirecardPaymentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="wirecardPaymentService">The wirecard payment service.</param>
        public HomeController(WirecardPaymentService wirecardPaymentService)
        {
            _wirecardPaymentService = wirecardPaymentService;
        }

        /// <summary>
        /// payment selection view
        /// </summary>
        /// <returns>IActionResult.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// the new payment action
        /// the fragmented calls below are refactored to the wirecard payment service
        /// </summary>
        /// <param name="endpointName">querystring parameter</param>
        /// <param name="paymentName">querystring parameter</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        /// Testdata:
        /// creditcard: cardNr.: 4200000000000018, CVC: 018, validTo: 01/23
        /// paypal:  email: buyer @wirecard.com, password: Einstein35
        /// iDeal: bank: Rabobank RABONL2U -or-  ING INGBNL2A
        /// sofortbanking: country: Deutschland, BIC: SFRTDE20XXX, accountNr.: 88888888
        public async Task<IActionResult> Payment(string endpointName, string paymentName)
        {
            // setup for demo payment call
            var paymentInfo = new PaymentInfo
            {
                AccountHolder = new AccountHolder { FirstName = "John", LastName = "Doe",
                    Address = new Address {
                        City ="Wien",
                        PostalCode = "1011",
                        Country = "AT",
                        Street1 = "Strasse 1"
                    }
                },
                Shipping = new Shipping
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Address = new Address
                    {
                        City = "Wien",
                        PostalCode = "1017",
                        Country = "AT",
                        Street1 = "Strasse 2"
                    }
                },
                RequestedAmount = new RequestedAmount { Currency = Currency.EUR, Value = 1.23m },
                RequestId = Guid.NewGuid().ToString(),
                PaymentName = paymentName,
                EndpointName = endpointName,
            };

            return Redirect(await _wirecardPaymentService.GetRedirectUrlFromWirecardAsync(paymentInfo));
        }



        /// <summary>
        /// elastic payment call for EPS
        /// Testdaten:
        /// Bank: Ärzte- und Apotheker Bank BWFBATW1XXX
        /// Just click to continue - no input needed.
        /// </summary>
        /// <returns>Task&lt;IActionResult&gt;.</returns>

        public async Task<IActionResult> EpsEpp()
        {
            var uri = new Uri("https://api-test.wirecard.com/engine/rest/paymentmethods");
            var username = "16390-testing";
            var password = "3!3013=D3fD8X7";
            var merchantAccountId = "1f629760-1a66-4f83-a6b4-6a35620b4a6d";
            var requestId = Guid.NewGuid().ToString();

            var request = $@"<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>
                <payment xmlns=""http://www.elastic-payments.com/schema/payment"">
                   <merchant-account-id>{merchantAccountId}</merchant-account-id>
                       <request-id>{requestId}</request-id>
                   <transaction-type>get-url</transaction-type>
                   <requested-amount currency=""EUR"">1.99</requested-amount>
                     <payment-methods>
                        <payment-method name=""eps"" />
                    </payment-methods>
                  <success-redirect-url>{GetRedirecturl(nameof(Success))}</success-redirect-url>
                   <cancel-redirect-url>{GetRedirecturl(nameof(Cancel))}</cancel-redirect-url>
                    <fail-redirect-url>{GetRedirecturl(nameof(Error))}</fail-redirect-url>
                </payment>";


            return await GetRedirectUrlFromWirecard(uri, username, password, request, RequestFormat.Xml);



        }

        /// <summary>
        /// Ealstic payment call for Google Pay / noch nicht komplett implementiert da Google Daten nötig sind (gleich wie bei Apple
        /// </summary>
        /// <param name="paymentToken">The payment token.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        public async Task<IActionResult> GooglePay(string paymentToken)
        {
           

            var uri = new Uri("https://api-test.wirecard.com/engine/rest/payments/");
            var username = "70000-APITEST-AP";
            var password = "qD2wzQ_hrc!8";
            var requestId = Guid.NewGuid().ToString();
            var redirecturl = string.Format("{0}://{1}{2}", Request.Scheme,
            Request.Host, "/checkout");

            var request = $@"<payment xmlns=""http://www.elastic-payments.com/schema/payment"">
                  <merchant-account-id>9fcacb0d-b46a-4ce2-867b-6723687fdba1</merchant-account-id>
                  <request-id>{requestId}</request-id>
                  <transaction-type>authorization</transaction-type>
                  <requested-amount currency=""EUR"">0.20</requested-amount>
                  <account-holder>
                    <first-name>John</first-name>
                    <last-name>Doe</last-name>
                    <email>tech.pdi-gw@wirecard.com</email>
                    <address>
                      <street1>Any Street</street1>
                      <city>Toronto</city>
                      <state>ON</state>
                      <country>CA</country>
                      <postal-code>M2H1C9</postal-code>
                    </address>
                  </account-holder>
                  <card>
                    <card-type>visa</card-type>
                  </card>
                  <cryptogram>
                   <cryptogram-value>{_wirecardPaymentService.Base64Encode(paymentToken)}</cryptogram-value>
                    <cryptogram-type>google-pay</cryptogram-type>
                  </cryptogram>
                  <ip-address>127.0.0.1</ip-address>
                  <entry-mode>mcommerce</entry-mode>
                  <payment-methods>
                    <payment-method name=""creditcard""/>
                  </payment-methods>
                </payment>";

            try
            {
                var client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{username}:{password}")));
                var response = await client.PostAsync(uri, new StringContent(request, Encoding.UTF8, "application/xml"));

                var responseData = await response.Content.ReadAsStringAsync();
                var xDoc = XDocument.Parse(responseData);
                var paymentResponse = new PaymentResponse { Payment = Wirecard.Models.Payment.From(XDocument.Parse(responseData)) };
                
                return Json(
                    new { Payload = _wirecardPaymentService.Base64Encode(JsonConvert.SerializeObject(paymentResponse)),
                        ReturnUrl = string.Concat("/home/", paymentResponse.Payment.TransactionState.Equals("success") ? "success" : "error")
                    });
            }
            catch (Exception)
            {

                throw;
            }

            


        }

        /// <summary>
        /// payment response action
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.String.</returns>
        public string Success(IFormCollection data)
        {
            PaymentResponse response = _wirecardPaymentService.GetPaymentResult(data);
          
            return $"{response}";


        }

        

        /// <summary>
        /// error response redirect
        /// </summary>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        public string Error(IFormCollection data)
        {
            PaymentResponse response = _wirecardPaymentService.GetPaymentResult(data);

            return $"{response}";
        }

        /// <summary>
        /// cancel response redirect
        /// </summary>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        public string Cancel(IFormCollection data)
        {
            PaymentResponse response = _wirecardPaymentService.GetPaymentResult(data);

            return $"{response}";
        }

        /// <summary>
        /// Ipns the specified response.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        public async Task<IActionResult> Ipn([FromBody]PaymentResponse response)
        {
            return Content($"data recieved: {response.Payment.CompletionTimeUtc}");
            
        }


        #region obsolte / old calls

     

      

      

        /// <summary>
        /// elastic payment call for Sofortüberweisung / Klarna
        /// TestDaten:
        /// BIC: Deutschland / SFRTDE20XXX
        /// Kontonummer: 88888888
        /// </summary>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        public async Task<IActionResult> KlarnaEpp()
        {
            var uri = new Uri("https://api-test.wirecard.com/engine/rest/paymentmethods");
            var username = "16390-testing";
            var password = "3!3013=D3fD8X7";
            var merchantAccountId = "6c0e7efd-ee58-40f7-9bbd-5e7337a052cd";
            var requestId = Guid.NewGuid().ToString();

            var request = $@"<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>
                <payment xmlns=""http://www.elastic-payments.com/schema/payment"">
                   <merchant-account-id>{merchantAccountId}</merchant-account-id>
                       <request-id>{requestId}</request-id>
                   <transaction-type>get-url</transaction-type>
                   <requested-amount currency=""EUR"">1.01</requested-amount>
                   <payment-methods>
                       <payment-method name=""sofortbanking"" />
                   </payment-methods>
                   <descriptor>FANZEE XRZ-1282</descriptor>
                   <success-redirect-url>{GetRedirecturl(nameof(Success))}</success-redirect-url>
                   <cancel-redirect-url>{GetRedirecturl(nameof(Cancel))}</cancel-redirect-url>
                </payment>";


            return await GetRedirectUrlFromWirecard(uri, username, password, request, RequestFormat.Xml);
        }

      
        /// <summary>
        /// legacy method for getting url from wirecard
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="payload">The payload.</param>
        /// <param name="requestType">Type of the request.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [Obsolete]
        private async Task<IActionResult> GetRedirectUrlFromWirecard(Uri uri, string username, string password, string payload, RequestFormat requestType)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = CreateAuthenticationHeader(username, password);

            var contentType = $"application/{requestType.ToString().ToLower()}";

            var response = await client.PostAsync(uri, new StringContent(payload, Encoding.UTF8, contentType));

            if (!response.IsSuccessStatusCode)
            {
                return Content(await response.Content.ReadAsStringAsync());
            }


            if (requestType == RequestFormat.Json)
            {
                return await CreateRedirectUrl(response);
            }
            else
            {
                return await CreateRedirectUrlXml(response);
            }
        }
        /// <summary>
        /// Creates the redirect URL.
        /// </summary>
        /// <param name="httpResponseMessage">The HTTP response message.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [Obsolete]
        private async Task<IActionResult> CreateRedirectUrl(HttpResponseMessage httpResponseMessage)
        {
            var responseData = await httpResponseMessage.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(responseData);
            var redirect = json.Value<string>("payment-redirect-url");
            return Redirect(redirect);
        }
        /// <summary>
        /// Creates the redirect URL XML.
        /// </summary>
        /// <param name="httpResponseMessage">The HTTP response message.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [Obsolete]
        private async Task<IActionResult> CreateRedirectUrlXml(HttpResponseMessage httpResponseMessage)
        {
            var responseData = await httpResponseMessage.Content.ReadAsStringAsync();
            var responseItem = XDocument.Parse(responseData);
            XNamespace ns = "http://www.elastic-payments.com/schema/payment";

            var redirect = responseItem.Root
                .Element(ns + "payment-methods")
                .Element(ns + "payment-method")
                .Attribute("url").Value;

            return Redirect(redirect);
        }
        /// <summary>
        /// Creates the authentication header.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>AuthenticationHeaderValue.</returns>
        [Obsolete]
        private AuthenticationHeaderValue CreateAuthenticationHeader(string username, string password)
        {
            return new AuthenticationHeaderValue(
            "Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{username}:{password}")));
        }
        /// <summary>
        /// Gets the redirecturl.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>System.String.</returns>
        [Obsolete]
        private string GetRedirecturl(string path)
        {
            return string.Concat(Request.Scheme, "://", Request.Host, "/", "home", "/", path);
        }

        #endregion
    }

   

   

   
}