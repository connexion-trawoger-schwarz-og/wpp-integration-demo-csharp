using Newtonsoft.Json;
using System.Xml.Serialization;

namespace DemoWebsite.Models
{
    /// <summary>
    /// payment method parent wrapper class
    /// </summary>
    public class PaymentMethods
    {
        /// <summary>
        /// the payment methods
        /// </summary>
        [XmlElement(ElementName = "payment-method")]
        [JsonProperty(PropertyName = "payment-method")]
        public PaymentMethod[] PaymentMethod { get; set; }
    }
}