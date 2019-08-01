// Copyright (c) 2019 connexion OG / Roman Wienicke
using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoWebsite.Models
{
    /// <summary>
    /// the endpoint for the wirecard request
    /// </summary>
    public class WirecardEndpoint
    {
        /// <summary>
        /// name of the endpoint
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// uri at wirecard
        /// </summary>
        public Uri Uri { get; set; }
        /// <summary>
        /// the success redirect url
        /// can be set absolute or relative
        /// if relative the host is computed for the current application
        /// </summary>
        public Uri SuccessRedirectUrl { get; set; }
        /// <summary>
        /// the fail redirect url
        /// can be set absolute or relative
        /// if relative the host is computed for the current application
        /// </summary>
        public Uri FailRedirectUrl { get; set; }
        /// <summary>
        /// the cancel redirect url
        /// can be set absolute or relative
        /// if relative the host is computed for the current application
        /// </summary>
        public Uri CancelRedirectUrl { get; set; }

        public WirecardPayment GetPaymentMethod(string name)
        {
            return PaymentMethods.FirstOrDefault(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// the payment methods
        /// </summary>
        public HashSet<WirecardPayment> PaymentMethods { get; set; }
    }



}