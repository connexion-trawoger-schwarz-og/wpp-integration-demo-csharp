// ***********************************************************************
// Assembly         : DemoWebsite
// Author           : r.wienicke
// Created          : 08-01-2019
//
// Last Modified By : r.wienicke
// Last Modified On : 08-05-2019
// ***********************************************************************
// <copyright file="PaymentResponse.cs" company="connexion e.solutions">
//     Copyright (c) connexion e.solutions. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
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
        /// <param name="response">The response.</param>
        /// <param name="type">The type.</param>
        /// <returns>PaymentResponse.</returns>
        public static PaymentResponse Parse(string response, RequestFormat type)
        {
            if (type == RequestFormat.Json)
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
        /// <value>The payment.</value>
        public Payment Payment { get; set; }


    }
}