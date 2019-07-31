﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;

namespace DemoWebsite.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// payment selection view
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Kreditkarten
        /// Testdaten: 
        /// Nr: 4200000000000018, CVC 018, validTo: 01/23
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CreditCard()
        {
            var uri = new Uri("https://wpp-test.wirecard.com/api/payment/register");
            var username = "70000-APIDEMO-CARD";
            var password = "ohysS0-dvfMx";
            var merchantAccountId = "7a6dd74f-06ab-4f3f-a864-adc52687270a";
            var requestId = Guid.NewGuid().ToString();
            var paymentMethod = "creditcard";
            
            var request = $@"{{
                  ""payment"": {{
                                ""merchant-account-id"": {{
                                    ""value"": ""{merchantAccountId}""
                                }},
                    ""request-id"": ""{requestId}"",
                    ""transaction-type"": ""authorization"",
                    ""requested-amount"": {{
                                    ""value"": 10,
                      ""currency"": ""EUR""
                    }},
                    ""account-holder"": {{
                                    ""first-name"": ""John"",
                      ""last-name"": ""Doe""
                    }},
                    ""payment-methods"": {{
                                    ""payment-method"": [
                                      {{
                          ""name"": ""{paymentMethod}""
                        }}
                      ]
                    }},
                    ""success-redirect-url"": ""{GetRedirecturl(nameof(Success))}"",
                    ""fail-redirect-url"": ""{GetRedirecturl(nameof(Error))}"",
                    ""cancel-redirect-url"": ""{GetRedirecturl(nameof(Cancel))}""
                  }}
                }}";

            return await GetRedirectUrlFromWirecard(uri, username, password, request, RequestType.Json);

        }





        /// <summary>
        ///Paypal
        /// Testdaten:
        /// Email buyer @wirecard.com
        /// Password Einstein35
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> PayPal()
        {
            var uri = new Uri("https://wpp-test.wirecard.com/api/payment/register");
            var username = "70000-APITEST-AP";
            var password = "qD2wzQ_hrc!8";
            var merchantAccountId = "9abf05c1-c266-46ae-8eac-7f87ca97af28";
            var requestId = Guid.NewGuid().ToString();
            var paymentMethod = "paypal";
            var request = $@"{{
                  ""payment"": {{
                                ""merchant-account-id"": {{
                                    ""value"": ""{merchantAccountId}""
                                }},
                    ""request-id"": ""{requestId}"",
                    ""transaction-type"": ""order"",
                    ""requested-amount"": {{
                                    ""value"": 1,
                      ""currency"": ""EUR""
                    }},
                    ""account-holder"": {{
                                    ""first-name"": ""John"",
                      ""last-name"": ""Doe""
                    }},
                    ""payment-methods"": {{
                                    ""payment-method"": [
                                      {{
                          ""name"": ""{paymentMethod}""
                        }}
                      ]
                    }},
                    ""success-redirect-url"": ""{GetRedirecturl(nameof(Success))}"",
                    ""fail-redirect-url"": ""{GetRedirecturl(nameof(Error))}"",
                    ""cancel-redirect-url"": ""{GetRedirecturl(nameof(Cancel))}""
                  }}
                }}";

            return await GetRedirectUrlFromWirecard(uri, username, password, request, RequestType.Json);
        }

        /// <summary>
        /// IDeal
        /// Testdaten:
        /// Rabobank RABONL2U
        /// ING INGBNL2A
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> IDeal()
        {
            var uri = new Uri("https://wpp-test.wirecard.com/api/payment/register");
            var username = "70000-APITEST-AP";
            var password = "qD2wzQ_hrc!8";
            var requestId = Guid.NewGuid().ToString();
            var merchantAccountId = "adb45327-170a-460b-9810-9008e9772f5f";
            var paymentMethod = "ideal";
            
            var request = $@"{{
                  ""payment"": {{
                                ""merchant-account-id"": {{
                                    ""value"": ""{merchantAccountId}""
                                }},
                    ""request-id"": ""{requestId}"",
                    ""transaction-type"": ""debit"",
                    ""requested-amount"": {{
                                    ""value"": 1.23,
                      ""currency"": ""EUR""
                    }},
                    ""payment-methods"": {{
                                    ""payment-method"": [
                                      {{
                          ""name"": ""{paymentMethod}""
                        }}
                      ]
                    }},
                    ""success-redirect-url"": ""{GetRedirecturl(nameof(Success))}"",
                    ""fail-redirect-url"": ""{GetRedirecturl(nameof(Error))}"",
                    ""cancel-redirect-url"": ""{GetRedirecturl(nameof(Cancel))}""
                  }}
                }}";


            return await GetRedirectUrlFromWirecard(uri, username, password, request, RequestType.Json);

        }

        /// <summary>
        /// Sofortüberweisung / Klarna
        /// TestDaten: 
        /// BIC: Deutschland / SFRTDE20XXX 
        /// Kontonummer: 88888888
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Klarna()
        {
            var uri = new Uri("https://wpp-test.wirecard.com/api/payment/register");
            var username = "70000-APITEST-AP";
            var password = "qD2wzQ_hrc!8";
            var merchantAccountId = "f19d17a2-01ae-11e2-9085-005056a96a54";
            var requestId = Guid.NewGuid().ToString();
            var paymentMethod = "sofortbanking";

            var request = $@"{{
                  ""payment"": {{
                                ""merchant-account-id"": {{
                                    ""value"": ""{merchantAccountId}""
                                }},
                    ""request-id"": ""{requestId}"",
                    ""transaction-type"": ""debit"",
                    ""requested-amount"": {{
                                    ""value"": 1.23,
                      ""currency"": ""EUR""
                    }},
                    ""payment-methods"": {{
                                    ""payment-method"": [
                                      {{
                          ""name"": ""{paymentMethod}""
                        }}
                      ]
                    }},
                    ""descriptor"": ""test"",
                    ""success-redirect-url"": ""{GetRedirecturl(nameof(Success))}"",
                    ""fail-redirect-url"": ""{GetRedirecturl(nameof(Error))}"",
                    ""cancel-redirect-url"": ""{GetRedirecturl(nameof(Cancel))}""
                  }}
                }}";


            return await GetRedirectUrlFromWirecard(uri, username, password, request, RequestType.Json);
        }

        /// <summary>
        /// Sofortüberweisung / Klarna
        /// TestDaten: 
        /// BIC: Deutschland / SFRTDE20XXX 
        /// Kontonummer: 88888888
        /// </summary>
        /// <returns></returns>
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


            return await GetRedirectUrlFromWirecard(uri, username, password, request, RequestType.Xml);
        }

        /// <summary>
        /// EPS
        /// Testdaten:
        /// Bank: Ärzte- und Apotheker Bank BWFBATW1XXX
        /// Just click to continue - no input needed.
        /// </summary>
        /// <returns></returns>

        public async Task<IActionResult> Eps()
        {

            var uri = new Uri("https://wpp-test.wirecard.com/api/payment/register");
            var username = "16390-testing";
            var password = "3!3013=D3fD8X7";
            var merchantAccountId = "1f629760-1a66-4f83-a6b4-6a35620b4a6d";
            var requestId = Guid.NewGuid().ToString();
            var paymentMethod = "eps";

            var request = $@"{{
                  ""payment"": {{
                                ""merchant-account-id"": {{
                                    ""value"": ""{merchantAccountId}""
                                }},
                    ""request-id"": ""{requestId}"",
                    ""transaction-type"": ""debit"",
                    ""requested-amount"": {{
                        ""value"": 10,
                        ""currency"": ""EUR""
                    }},
                    ""account-holder"": {{
                        ""first-name"": ""John"",
                        ""last-name"": ""Doe""
                    }},
                    ""payment-methods"": {{
                                    ""payment-method"": [
                                      {{
                          ""name"": ""{paymentMethod}""
                        }}
                      ]
                    }},
                    ""success-redirect-url"": ""{GetRedirecturl(nameof(Success))}"",
                    ""fail-redirect-url"": ""{GetRedirecturl(nameof(Error))}"",
                    ""cancel-redirect-url"": ""{GetRedirecturl(nameof(Cancel))}""
                  }}
                }}";

            return await GetRedirectUrlFromWirecard(uri, username, password, request, RequestType.Json);


        }


        /// <summary>
        /// EPS
        /// Testdaten:
        /// Bank: Ärzte- und Apotheker Bank BWFBATW1XXX
        /// Just click to continue - no input needed.
        /// </summary>
        /// <returns></returns>

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


            return await GetRedirectUrlFromWirecard(uri, username, password, request, RequestType.Xml);



        }

        /// <summary>
        /// Google Pay / noch nicht komplett implementiert da Google Daten nötig sind (gleich wie bei Apple
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GooglePay()
        {
            var baseUri = new Uri("https://api-test.wirecard.com/engine/rest/");
            var username = "70000-APITEST-AP";
            var password = "qD2wzQ_hrc!8";
            var requestId = Guid.NewGuid().ToString();
            var redirecturl = string.Format("{0}://{1}{2}", Request.Scheme,
            Request.Host, "/checkout");

            var request = $@"<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>
                <payment xmlns=""http://www.elastic-payments.com/schema/payment"">
                   <merchant-account-id>3a3d15ec-197a-4958-890e-9843f86207ee</merchant-account-id>
                       <request-id>{requestId}</request-id>
                   <transaction-type>get-url</transaction-type>
                   <requested-amount currency=""EUR"">1.01</requested-amount>
                   <payment-methods>
                       <payment-method name=""sofortbanking"" />
                   </payment-methods>
                   <descriptor>FANZEE XRZ-1282</descriptor>
                   <success-redirect-url>{redirecturl}/{nameof(Success)}</success-redirect-url>
                   <cancel-redirect-url>{redirecturl}/{nameof(Cancel)}</cancel-redirect-url>
                </payment>";



            var client = new HttpClient();
            client.BaseAddress = baseUri;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{username}:{password}")));
            var response = await client.PostAsync("paymentmethods", new StringContent(request, Encoding.UTF8, "application/xml"));

            var responseData = await response.Content.ReadAsStringAsync();
            var responseItem = XDocument.Parse(responseData);
            XNamespace ns = "http://www.elastic-payments.com/schema/payment";

            var redirect = responseItem.Root
                .Element(ns + "payment-methods")
                .Element(ns + "payment-method")
                .Attribute("url").Value;

            return Redirect(redirect);


        }



        /// <summary>
        /// response for elastic paymentes
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        //public string SuccessEpp(IFormCollection data)
        //{

        //    string type = data["psp_name"];
        //    string custom_css_url = data["custom_css_url"];
        //    string locale = data["locale"];
        //    string responseBase64 = data["eppresponse"];
        //    //psp_name, custom_css_url, eppresponse, locale


        //    var response = Encoding.UTF8.GetString(Convert.FromBase64String(responseBase64));

        //    return string.Concat($"response:{response}");
        //}

        /// <summary>
        /// default response
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Success(IFormCollection data)
        {


            if (data["eppresponse"] == StringValues.Empty)
            {

                string signatureBase64 = data["response-signature-base64"];
                string signatureAlgorithm = data["response-signature-altorithm"];
                string responseBase64 = data["response-base64"];
                var signature = Encoding.UTF8.GetString(Convert.FromBase64String(signatureBase64));
                var response = DecodeResponse(signatureBase64, signatureAlgorithm, responseBase64);

             

                return string.Concat($"{response}");
            }
            else
            {
                string type = data["psp_name"];
                string custom_css_url = data["custom_css_url"];
                string locale = data["locale"];
                string responseBase64 = data["eppresponse"];
                string response = Encoding.UTF8.GetString(Convert.FromBase64String(responseBase64));
                return string.Concat($"{response}");
            }




        }

        private string DecodeResponse(string signatureBase64, string signatureAlgorithm, string responseBase64)
        {
            var bytes = Convert.FromBase64String(responseBase64);
            return Encoding.UTF8.GetString(bytes);
        }
            
        public async Task<IActionResult> Error()
        {
            return Content("Error");
        }
        public async Task<IActionResult> Cancel()
        {
            return Content("Cancel");
        }

        private async Task<IActionResult> GetRedirectUrlFromWirecard(Uri uri, string username, string password, string payload, RequestType requestType)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = CreateAuthenticationHeader(username, password);

            var contentType = $"application/{requestType.ToString().ToLower()}";

            var response = await client.PostAsync(uri, new StringContent(payload, Encoding.UTF8, contentType));

            if (!response.IsSuccessStatusCode)
            {
                return Content(await response.Content.ReadAsStringAsync());
            }


            if (requestType == RequestType.Json)
            {
                return await CreateRedirectUrl(response);
            }
            else
            {
                return await CreateRedirectUrlXml(response);
            }
        }

        private async Task<IActionResult> CreateRedirectUrl(HttpResponseMessage httpResponseMessage)
        {
            var responseData = await httpResponseMessage.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(responseData);
            var redirect = json.Value<string>("payment-redirect-url");
            return Redirect(redirect);
        }

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

        private AuthenticationHeaderValue CreateAuthenticationHeader(string username, string password)
        {
            return new AuthenticationHeaderValue(
            "Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{username}:{password}")));
        }

        private string GetRedirecturl(string path)
        {
            return string.Concat(Request.Scheme, "://", Request.Host, "/", "home", "/", path);
        }

        
    }

    public enum RequestType
    {
        Json, Xml
    }
}