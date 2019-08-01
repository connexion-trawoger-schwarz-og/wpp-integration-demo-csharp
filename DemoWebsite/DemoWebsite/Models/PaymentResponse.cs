using Newtonsoft.Json;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DemoWebsite.Models
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
            }


            var doc = XDocument.Parse(response);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Payment));
            
            using (var reader = doc.CreateReader())
            {
                return new PaymentResponse { Payment = (Payment)xmlSerializer.Deserialize(reader) };
            }

        }

        /// <summary>
        /// the payment response
        /// </summary>
        public Payment Payment { get; set; }


    }
}