using Newtonsoft.Json;
using System.Xml.Serialization;

namespace DemoWebsite.Models
{
    /// <summary>
    /// card token used for accessing the same card without need of real number
    /// </summary>
    public class CardToken
    {
        /// <summary>
        /// the token id to be sent with the request to access the stored card
        /// </summary>
        [XmlElement(ElementName = "token-id")]
        [JsonProperty(PropertyName = "token-id")]
        public string TokenId { get; set; }

        /// <summary>
        /// the masked card number
        /// </summary>
        [XmlElement(ElementName = "masked-account-number")]
        [JsonProperty(PropertyName = "masked-account-number")]
        public string MaskedAccountNumber { get; set; }
    }
}