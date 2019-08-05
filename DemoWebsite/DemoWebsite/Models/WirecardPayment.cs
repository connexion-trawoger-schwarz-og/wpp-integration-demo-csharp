// ***********************************************************************
// Assembly         : DemoWebsite
// Author           : r.wienicke
// Created          : 08-01-2019
//
// Last Modified By : r.wienicke
// Last Modified On : 08-05-2019
// ***********************************************************************
// <copyright file="WirecardPayment.cs" company="connexion e.solutions">
//     Copyright (c) connexion e.solutions. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace DemoWebsite.Models
{
    /// <summary>
    /// wirecard payment configuration
    /// </summary>
    public class WirecardPayment
    {
        /// <summary>
        /// the payment name
        /// this name is sent to wirecard so the name must match the wirecard payment name
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
        /// <summary>
        /// username for wirecard
        /// </summary>
        /// <value>The username.</value>
        public string Username { get; set; }
        /// <summary>
        /// password for wirecard
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; set; }
        /// <summary>
        /// the merchant account id for wirecard and this payment method
        /// </summary>
        /// <value>The merchant account identifier.</value>
        public string MerchantAccountId { get; set; }
        /// <summary>
        /// the request type (json or xml)
        /// </summary>
        /// <value>The type of the request.</value>
        public RequestFormat RequestType { get; set; }

        /// <summary>
        /// the default transaction type
        /// important: check if it's the right one for your purposes!
        /// </summary>
        /// <value>The default type of the transaction.</value>
        public TransactionType DefaultTransactionType { get; set; }

    }



}