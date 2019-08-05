// Copyright (c) 2019 connexion OG / Roman Wienicke
using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace DemoWebsite.Models
{
    /// <summary>
    /// payment data
    /// </summary>
    [XmlRoot(ElementName = "payment", Namespace = "http://www.elastic-payments.com/schema/payment")]
    public class Payment
    {
        /// <summary>
        /// the transaction id
        /// </summary>
        [XmlElement(ElementName = "transaction-id")]
        [JsonProperty(PropertyName = "transaction-id")]
        public string TransactionId { get; set; }

        /// <summary>
        /// the parent transaction id
        /// referes to transactionids from other requests
        /// </summary>
        [XmlElement(ElementName = "parent-transaction-id")]
        [JsonProperty(PropertyName = "parent-transaction-id")]
        public string ParentTransactionId { get; set; }

        /// <summary>
        /// the merchant request id
        /// must be unique for each request
        /// </summary>
        [XmlElement(ElementName = "request-id")]
        [JsonProperty(PropertyName = "request-id")]
        public string RequestId { get; set; }

        /// <summary>
        /// the state of the transaction
        /// </summary>
        [XmlElement(ElementName = "transaction-state")]
        [JsonProperty(PropertyName = "transaction-state")]
        public string TransactionState { get; set; }

        /// <summary>
        /// completion time
        /// </summary>
        [XmlElement(ElementName = "completion-time-stamp")]
        [JsonProperty(PropertyName = "completion-time-stamp")]
        public long? CompletionTimeStamp { get; set; }

        private DateTime? _CompletionTime;
        public DateTime? CompletionTime
        {
            get
            {
                if (CompletionTimeStamp.HasValue && _CompletionTime == null)
                {
                    DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(CompletionTimeStamp.Value);
                    _CompletionTime = dateTimeOffset.UtcDateTime.ToLocalTime();
                }
                return _CompletionTime;
            }
        }

        /// <summary>
        /// the requested amount
        /// </summary>
        [XmlElement(ElementName = "requested-amount")]
        [JsonProperty(PropertyName = "requested-amount")]
        public RequestedAmount RequestedAmount { get; set; }

        /// <summary>
        /// the statuses
        /// </summary>
        [JsonProperty(PropertyName = "statuses")]
        public Statuses Statuses { get; set; }

        /// <summary>
        /// the authorization code
        /// </summary>
        [XmlElement(ElementName = "authorization-code")]
        [JsonProperty(PropertyName = "authorization-code")]
        public string AuthorizationCode { get; set; }

        /// <summary>
        /// the wirecard merchant account id
        /// </summary>
        [XmlElement(ElementName = "merchant-account-id")]
        [JsonProperty(PropertyName = "merchant-account-id")]
        public MerchantAccountId MerchantAccountId { get; set; }

        /// <summary>
        /// the cancle redirect url
        /// can be set absolute or relative
        /// if relative the host is computed for the current application
        /// </summary>
        [XmlElement(ElementName = "cancel-redirect-url")]
        [JsonProperty(PropertyName = "cancel-redirect-url")]
        public string CancelRedirectUrl { get; set; }

        /// <summary>
        /// the fail redirect url
        /// can be set absolute or relative
        /// if relative the host is computed for the current application
        /// </summary>
        [XmlElement(ElementName = "fail-redirect-url")]
        [JsonProperty(PropertyName = "fail-redirect-url")]
        public string FailRedirectUrl { get; set; }

        /// <summary>
        /// the success redirect url
        /// can be set absolute or relative
        /// if relative the host is computed for the current application
        /// </summary>
        [XmlElement(ElementName = "success-redirect-url")]
        [JsonProperty(PropertyName = "success-redirect-url")]
        public string SuccessRedirectUrl { get; set; }

        /// <summary>
        /// the account holder
        /// </summary>
        [XmlElement(ElementName = "account-holder")]
        [JsonProperty(PropertyName = "account-holder")]
        public AccountHolder AccountHolder { get; set; }

        /// <summary>
        /// the shipping info
        /// </summary>
        [XmlElement(ElementName = "shipping")]
        [JsonProperty(PropertyName = "shipping")]
        public Shipping Shipping { get; set; }


        /// <summary>
        /// the payment methods
        /// </summary>
        [XmlElement(ElementName = "payment-methods")]
        [JsonProperty(PropertyName = "payment-methods")]
        public PaymentMethods PaymentMethods { get; set; }

        /// <summary>
        /// the transaction type / must be set in config or can be set in <see cref="PaymentInfo"></see>
        /// </summary>
        [XmlElement(ElementName = "transaction-type")]
        [JsonProperty(PropertyName = "transaction-type")]
        public string TransactionType { get; set; }

        /// <summary>
        /// the card token for getting stored card 
        /// </summary>
        [XmlElement(ElementName = "card-token")]
        [JsonProperty(PropertyName = "card-token")]
        public CardToken CardToken { get; set; }

        /// <summary>
        /// the app id
        /// </summary>
        [XmlElement(ElementName = "api-id")]
        [JsonProperty(PropertyName = "api-id")]
        public string ApiId { get; set; }

        /// <summary>
        /// the device
        /// </summary>
        [JsonProperty(PropertyName = "device")]
        public Device Device { get; set; }

        /// <summary>
        /// the descriptor
        /// </summary>
        [JsonProperty(PropertyName = "descriptor")]
        public string Descriptor { get; set; }

        [JsonProperty(PropertyName = "notifications")]
        public Notifications Notifications { get; set; }

    }
}