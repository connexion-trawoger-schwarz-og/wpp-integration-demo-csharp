// Copyright (c) 2019 connexion OG / Roman Wienicke
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
        public string Name { get; set; }
        /// <summary>
        /// username for wirecard
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// password for wirecard
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// the merchant account id for wirecard and this payment method
        /// </summary>
        public string MerchantAccountId { get; set; }
        /// <summary>
        /// the request type (json or xml)
        /// </summary>
        public RequestFormat RequestType { get; set; }

        /// <summary>
        /// the default transaction type
        /// important: check if it's the right one for your purposes!
        /// </summary>
        public TransactionType DefaultTransactionType { get; set; }

    }



}