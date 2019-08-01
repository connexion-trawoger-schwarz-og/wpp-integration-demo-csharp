// Copyright (c) 2019 connexion OG / Roman Wienicke

namespace DemoWebsite.Models
{
    /// <summary>
    /// the payment info 
    /// </summary>
    public class PaymentInfo
    {
        /// <summary>
        /// the request id 
        /// must be unique for each call
        /// </summary>
        public string RequestId { get; internal set; }

        /// <summary>
        /// the requseted amount
        /// </summary>
        public RequestedAmount RequestedAmount { get; set; }

        /// <summary>
        /// the card holder
        /// </summary>
        public AccountHolder AccountHolder { get; internal set; }

        /// <summary>
        /// the paymentname from config settings
        /// </summary>
        public string PaymentName { get; set; }
        /// <summary>
        /// the endpointname from config settings
        /// </summary>
        public string EndpointName { get; set; }

        /// <summary>
        /// the transaction type
        /// </summary>
        public TransactionType? TransactionType { get; set; }


    }


}
