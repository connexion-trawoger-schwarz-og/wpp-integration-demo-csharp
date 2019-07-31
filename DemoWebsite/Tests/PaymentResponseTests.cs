using DemoWebsite.Controllers;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace Tests
{
    public class Tests
    {

        /// <summary>
        /// simple tests for parsing of the result json
        /// </summary>
        /// <param name="responseFile"></param>
        /// <param name="type"></param>
        ///  xmlns="http://www.elastic-payments.com/schema/payment"
        [TestCase("paymentResponse_cc.json", "creditcard", RequestType.Json)]
        [TestCase("paymentResponse_eps.json", "eps")]
        [TestCase("paymentResponse_klarna.json", "sofortbanking", RequestType.Json)]
        [TestCase("paymentResponse_paypal.json", "paypal", RequestType.Json)]
        [TestCase("paymentResponse_klarna.xml", "sofortbanking", RequestType.Xml)]
        public void ParsePaymentResponseTest(string responseFile, string payment, RequestType type)
        {
            string content = GetFileInStartupPath(responseFile);
            var result = PaymentResponse.Parse(content, type);
            //Assert.IsTrue(result.Payment.PaymentMethods.PaymentMethod.FirstOrDefault().Name == payment);
        }

        private static string GetFileInStartupPath(string responseFile)
        {
            var fileName = Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), responseFile);
            var content = File.ReadAllText(fileName);
            return content;
        }


    }
}