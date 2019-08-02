// Copyright (c) 2019 connexion OG / Roman Wienicke
using DemoWebsite.Models;
using DemoWebsite.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DemoWebsite.Controllers
{
    /// <summary>
    /// startup controller
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// injected wirecard payment service
        /// </summary>
        private readonly WirecardPaymentService _wirecardPaymentService;

        public HomeController(WirecardPaymentService wirecardPaymentService)
        {
            _wirecardPaymentService = wirecardPaymentService;
        }

        /// <summary>
        /// payment selection view
        /// </summary>
        /// <returns></returns>
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
        /// Testdata:
        /// creditcard: cardNr.: 4200000000000018, CVC: 018, validTo: 01/23
        /// paypal:  email: buyer @wirecard.com, password: Einstein35
        /// iDeal: bank: Rabobank RABONL2U -or-  ING INGBNL2A
        /// sofortbanking: country: Deutschland, BIC: SFRTDE20XXX, accountNr.: 88888888
        /// <returns></returns>
        public async Task<IActionResult> Payment(string endpointName, string paymentName)
        {
            // setup for demo payment call
            var paymentInfo = new PaymentInfo
            {
                AccountHolder = new AccountHolder { FirstName = "John", LastName = "Doe",
                    Address = new Address {
                        City ="Innsbruck",
                        PostalCode = "6020",
                        Country = "AT",
                        Street1 = "Dr. Franz Werner Strasse 36"
                    }
                },
                Shipping = new Shipping
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Address = new Address
                    {
                        City = "Innsbruck",
                        PostalCode = "6020",
                        Country = "AT",
                        Street1 = "Dr. Franz Werner Strasse 36"
                    }
                },
                RequestedAmount = new RequestedAmount { Currency = Currency.EUR, Value = 1.23m },
                RequestId = Guid.NewGuid().ToString(),
                PaymentName = paymentName,
                EndpointName = endpointName,

            };

            return Redirect(await _wirecardPaymentService.GetRedirectUrlFromWirecard(paymentInfo));
        }



        /// <summary>
        /// elastic payment call for EPS
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

        public IActionResult GooglePayStart()
        {
            return View();
        }

        

        /// <summary>
        /// Ealstic payment call for Google Pay / noch nicht komplett implementiert da Google Daten nötig sind (gleich wie bei Apple
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GooglePay(GooglePaymentResponse data)
        {
            var sig = JsonConvert.SerializeObject(data.PaymentMethodData);
            

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
                   <cryptogram-value>{sig}</cryptogram-value>
                    <cryptogram-type>google-pay</cryptogram-type>
                  </cryptogram>
                  <ip-address>127.0.0.1</ip-address>
                  <entry-mode>mcommerce</entry-mode>
                  <payment-methods>
                    <payment-method name=""creditcard""/>
                  </payment-methods>
                </payment>";



            var client = new HttpClient();
            
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{username}:{password}")));
            var response = await client.PostAsync(uri, new StringContent(request, Encoding.UTF8, "application/xml"));

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
        /// payment response action
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Success(IFormCollection data)
        {
            // data from elastc payment
            if (data["eppresponse"] == StringValues.Empty)
            {

                string signatureBase64 = data["response-signature-base64"];
                string signatureAlgorithm = data["response-signature-altorithm"];
                string responseBase64 = data["response-base64"];
                var signature = Encoding.UTF8.GetString(Convert.FromBase64String(signatureBase64));
                var response = DecodeResponse(signatureBase64, signatureAlgorithm, responseBase64);



                return $"{response}";
            }
            // data from wirecard REST
            else
            {
                string type = data["psp_name"];
                string custom_css_url = data["custom_css_url"];
                string locale = data["locale"];
                string responseBase64 = data["eppresponse"];
                string response = Encoding.UTF8.GetString(Convert.FromBase64String(responseBase64));
                return $"{response}";
            }




        }

        /// <summary>
        /// decode wirecard response data
        /// </summary>
        /// <param name="signatureBase64"></param>
        /// <param name="signatureAlgorithm"></param>
        /// <param name="responseBase64"></param>
        /// <returns></returns>
        private string DecodeResponse(string signatureBase64, string signatureAlgorithm, string responseBase64)
        {
            var bytes = Convert.FromBase64String(responseBase64);
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// error response redirect
        /// </summary>
        /// <returns></returns>
        public IActionResult Error()
        {
            return Content("Error");
        }

        /// <summary>
        /// cancel response redirect
        /// </summary>
        /// <returns></returns>
        public IActionResult Cancel()
        {
            return Content("Cancel");
        }

        #region obsolte / old calls

        /// <summary>
        /// legacy call for credit card payment
        /// Testdaten: 
        /// Nr: 4200000000000018, CVC 018, validTo: 01/23
        /// </summary>
        /// <returns></returns>
        [Obsolete]
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
        ///legacy call for paypal
        /// Testdaten:
        /// Email buyer @wirecard.com
        /// Password Einstein35
        /// </summary>
        /// <returns></returns>
        [Obsolete]
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
        /// legacy call for IDeal
        /// Testdaten:
        /// Rabobank RABONL2U
        /// ING INGBNL2A
        /// </summary>
        /// <returns></returns>
        [Obsolete]
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
        /// legacy call for Sofortüberweisung / Klarna
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
        /// elastic payment call for Sofortüberweisung / Klarna
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
        /// legacy call for EPS
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
        /// legacy method for getting url from wirecard
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="payload"></param>
        /// <param name="requestType"></param>
        /// <returns></returns>
        [Obsolete]
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
        [Obsolete]
        private async Task<IActionResult> CreateRedirectUrl(HttpResponseMessage httpResponseMessage)
        {
            var responseData = await httpResponseMessage.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(responseData);
            var redirect = json.Value<string>("payment-redirect-url");
            return Redirect(redirect);
        }
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
        [Obsolete]
        private AuthenticationHeaderValue CreateAuthenticationHeader(string username, string password)
        {
            return new AuthenticationHeaderValue(
            "Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{username}:{password}")));
        }
        [Obsolete]
        private string GetRedirecturl(string path)
        {
            return string.Concat(Request.Scheme, "://", Request.Host, "/", "home", "/", path);
        }

        #endregion
    }

    public class GooglePaymentResponse
    {
        public int ApiVersionMinor { get; set; }
        public int ApiVersion { get; set; }
        public PaymentMethodData PaymentMethodData { get; set; }
    }

    public class PaymentMethodData
    {
        public string Description { get; set; }
        public TokenizationData TokenizationData { get; set; }
        public string Type { get; set; }
        public Info Info { get; set; }
    }

    public class Info
    {
        public string CardNetwork { get; set; }
        public string CardDetails { get; set; }
    }

    public class TokenizationData
    {
        public string Type { get; set; }
        public string Token { get; set; }

       
    }
}