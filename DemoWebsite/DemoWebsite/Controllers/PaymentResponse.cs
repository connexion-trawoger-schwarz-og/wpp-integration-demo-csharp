using Newtonsoft.Json;
using System;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DemoWebsite.Controllers
{
    /// <summary>
    /// Response from Wirecard (parsed as PaymentResponse Object)
    /// </summary>
    public class PaymentResponse
    {
        /// <summary>
        /// parse of response string
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static PaymentResponse Parse(string response, RequestType type)
        {
            if (type == RequestType.Json)
            {
                return JsonConvert.DeserializeObject<PaymentResponse>(response);
                //var obj = JsonConvert.DeserializeObject<PaymentResponse>(response);
                //XmlSerializer xmlSerializer1 = new XmlSerializer(typeof(Payment));
                //using (var writer = new StreamWriter("d:\\data.xml"))
                //{
                //    xmlSerializer1.Serialize(writer, obj.Payment);
                //}
            }


            var doc = XDocument.Parse(response);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Payment));
            
            using (var reader = doc.CreateReader())
            {
                return new PaymentResponse { Payment = (Payment)xmlSerializer.Deserialize(reader) };
            }

        }

        
        public Payment Payment { get; set; }


    }

    /*
     classes for converting the json result string to .net
     use of [JsonProperty(PropertyName = "name-of-property")]
     because c# .net does not allew "-" in names.
    */

    /// <summary>
    /// Root for payment result
    /// </summary>
    [XmlRoot(ElementName = "payment", Namespace = "http://www.elastic-payments.com/schema/payment")]
    public class Payment
    {
        [XmlElement(ElementName = "transaction-id")]
        [JsonProperty(PropertyName = "transaction-id")]
        public string TransactionId { get; set; }

        [XmlElement(ElementName = "request-id")]
        [JsonProperty(PropertyName = "request-id")]
        public string RequestId { get; set; }

        [XmlElement(ElementName = "transaction-state")]
        [JsonProperty(PropertyName = "transaction-state")]
        public string TransactionState { get; set; }

        [XmlElement(ElementName = "completion-time-stamp")]
        [JsonProperty(PropertyName = "completion-time-stamp")]
        public DateTime CompletionTime { get; set; }

        [XmlElement(ElementName = "requested-amount")]
        [JsonProperty(PropertyName = "requested-amount")]
        public RequestedAmount RequestedAmount { get; set; }


        public Statuses Statuses { get; set; }

        [XmlElement(ElementName = "authorization-code")]
        [JsonProperty(PropertyName = "authorization-code")]
        public string AuthorizationCode { get; set; }

        [XmlElement(ElementName = "merchant-account-id")]
        [JsonProperty(PropertyName = "merchant-account-id")]
        public MerchantAccountId MerchantAccountId { get; set; }

        [XmlElement(ElementName = "cancel-redirect-url")]
        [JsonProperty(PropertyName = "cancel-redirect-url")]
        public string CancelRedirectUrl { get; set; }

        [XmlElement(ElementName = "fail-redirect-url")]
        [JsonProperty(PropertyName = "fail-redirect-url")]
        public string FailRedirectUrl { get; set; }

        [XmlElement(ElementName = "success-redirect-url")]
        [JsonProperty(PropertyName = "success-redirect-url")]
        public string SuccessRedirectUrl { get; set; }

        [XmlElement(ElementName = "account-holder")]
        [JsonProperty(PropertyName = "account-holder")]
        public AccountHolder AccountHolder { get; set; }

        [XmlElement(ElementName = "payment-methods")]
        [JsonProperty(PropertyName = "payment-methods")]
        public PaymentMethods PaymentMethods { get; set; }

        [XmlElement(ElementName = "transaction-type")]
        [JsonProperty(PropertyName = "transaction-type")]
        public string TransactionType { get; set; }

        [XmlElement(ElementName = "card-token")]
        [JsonProperty(PropertyName = "card-token")]
        public CardToken CardToken { get; set; }

        [XmlElement(ElementName = "api-id")]
        [JsonProperty(PropertyName = "api-id")]
        public string ApiId { get; set; }

        public Device Device { get; set; }

        public string Descriptor { get; set; }
        
    }

    public class RequestedAmount
    {
        public string Currency { get; set; }
        public decimal Value { get; set; }
    }


    public class Statuses
    {
        public Status[] Status { get; set; }
    }

    public class Status
    {
        public string Description { get; set; }
        public string Severity { get; set; }
        public string Code { get; set; }
    }




    public class MerchantAccountId
    {
        public string Value { get; set; }
    }

    public class AccountHolder
    {
        [XmlElement(ElementName = "account-info")]
        [JsonProperty(PropertyName = "account-info")]
        public AccountInfo AccountInfo { get; set; }

        [XmlElement(ElementName = "last-name")]
        [JsonProperty(PropertyName = "last-name")]
        public string LastName { get; set; }

        [XmlElement(ElementName = "first-name")]
        [JsonProperty(PropertyName = "first-name")]
        public string FirstName { get; set; }
    }

    public class AccountInfo
    {
    }

    public class PaymentMethods
    {
        [XmlElement(ElementName = "payment-method")]
        [JsonProperty(PropertyName = "payment-method")]
        public PaymentMethod[] PaymentMethod { get; set; }
    }

    public class PaymentMethod
    {
        public Payload Payload { get; set; }
        public string Name { get; set; }
    }

    public class Payload
    {
    }

    public class CardToken
    {
        [XmlElement(ElementName = "token-id")]
        [JsonProperty(PropertyName = "token-id")]
        public string TokenId { get; set; }

        [XmlElement(ElementName = "masked-account-number")]
        [JsonProperty(PropertyName = "masked-account-number")]
        public string MaskedAccountNumber { get; set; }
    }

    public class Device
    {
        public string Fingerprint { get; set; }
    }
}