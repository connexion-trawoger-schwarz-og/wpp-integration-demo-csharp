// ***********************************************************************
// Assembly         : DemoWebsite
// Author           : r.wienicke
// Created          : 08-01-2019
//
// Last Modified By : r.wienicke
// Last Modified On : 08-05-2019
// ***********************************************************************
// <copyright file="Payment.cs" company="connexion e.solutions">
//     Copyright (c) connexion e.solutions. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Wirecard.Models
{
    /// <summary>
    /// payment data
    /// </summary>
    [XmlRoot(ElementName = "payment", Namespace = "http://www.elastic-payments.com/schema/payment")]
    public class Payment
    {
        public static Payment From(XDocument document)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Payment));

            using (var reader = document.CreateReader())
            {
                return (Payment)xmlSerializer.Deserialize(reader);
            }
        }


        /// <summary>
        /// the transaction id
        /// </summary>
        /// <value>The transaction identifier.</value>
        [XmlElement(ElementName = "transaction-id")]
        [JsonProperty(PropertyName = "transaction-id")]
        public string TransactionId { get; set; }

        /// <summary>
        /// the parent transaction id
        /// referes to transactionids from other requests
        /// </summary>
        /// <value>The parent transaction identifier.</value>
        [XmlElement(ElementName = "parent-transaction-id")]
        [JsonProperty(PropertyName = "parent-transaction-id")]
        public string ParentTransactionId { get; set; }

        /// <summary>
        /// the merchant request id
        /// must be unique for each request
        /// </summary>
        /// <value>The request identifier.</value>
        [XmlElement(ElementName = "request-id")]
        [JsonProperty(PropertyName = "request-id")]
        public string RequestId { get; set; }

        /// <summary>
        /// the state of the transaction
        /// </summary>
        /// <value>The state of the transaction.</value>
        [XmlElement(ElementName = "transaction-state")]
        [JsonProperty(PropertyName = "transaction-state")]
        public string TransactionState { get; set; }


        /// <summary>
        /// The completion time stamp
        /// </summary>
        private string _CompletionTimeStamp;
        /// <summary>
        /// completion time
        /// </summary>
        /// <value>The completion time stamp.</value>
        [XmlElement(ElementName = "completion-time-stamp")]
        [JsonProperty(PropertyName = "completion-time-stamp")]
        public string CompletionTimeStamp
        {
            get
            {
                return _CompletionTimeStamp;
            }
            set
            {
                _CompletionTimeStamp = value;
                if (!DateTime.TryParse(value, out DateTime completionTime))
                {
                    DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(CompletionTimeStamp));
                    CompletionTimeUtc = dateTimeOffset.UtcDateTime;
                }
                else
                {
                    CompletionTimeUtc = completionTime;
                }
            }
        }

        /// <summary>
        /// Gets the completion time UTC.
        /// </summary>
        /// <value>The completion time UTC.</value>
        [XmlIgnore]
        public DateTime? CompletionTimeUtc
        {
            get;
            private set;
        }

        /// <summary>
        /// the requested amount
        /// </summary>
        /// <value>The requested amount.</value>
        [XmlElement(ElementName = "requested-amount")]
        [JsonProperty(PropertyName = "requested-amount")]
        public RequestedAmount RequestedAmount { get; set; }

        /// <summary>
        /// the statuses
        /// </summary>
        /// <value>The statuses.</value>
        [JsonProperty(PropertyName = "statuses")]
        public Statuses Statuses { get; set; }

        /// <summary>
        /// the authorization code
        /// </summary>
        /// <value>The authorization code.</value>
        [XmlElement(ElementName = "authorization-code")]
        [JsonProperty(PropertyName = "authorization-code")]
        public string AuthorizationCode { get; set; }

        /// <summary>
        /// the wirecard merchant account id
        /// </summary>
        /// <value>The merchant account identifier.</value>
        [XmlElement(ElementName = "merchant-account-id")]
        [JsonProperty(PropertyName = "merchant-account-id")]
        public MerchantAccountId MerchantAccountId { get; set; }

        /// <summary>
        /// the cancle redirect url
        /// can be set absolute or relative
        /// if relative the host is computed for the current application
        /// </summary>
        /// <value>The cancel redirect URL.</value>
        [XmlElement(ElementName = "cancel-redirect-url")]
        [JsonProperty(PropertyName = "cancel-redirect-url")]
        public string CancelRedirectUrl { get; set; }

        /// <summary>
        /// the fail redirect url
        /// can be set absolute or relative
        /// if relative the host is computed for the current application
        /// </summary>
        /// <value>The fail redirect URL.</value>
        [XmlElement(ElementName = "fail-redirect-url")]
        [JsonProperty(PropertyName = "fail-redirect-url")]
        public string FailRedirectUrl { get; set; }

        /// <summary>
        /// the success redirect url
        /// can be set absolute or relative
        /// if relative the host is computed for the current application
        /// </summary>
        /// <value>The success redirect URL.</value>
        [XmlElement(ElementName = "success-redirect-url")]
        [JsonProperty(PropertyName = "success-redirect-url")]
        public string SuccessRedirectUrl { get; set; }

        /// <summary>
        /// the account holder
        /// </summary>
        /// <value>The account holder.</value>
        [XmlElement(ElementName = "account-holder")]
        [JsonProperty(PropertyName = "account-holder")]
        public AccountHolder AccountHolder { get; set; }

        /// <summary>
        /// the shipping info
        /// </summary>
        /// <value>The shipping.</value>
        [XmlElement(ElementName = "shipping")]
        [JsonProperty(PropertyName = "shipping")]
        public Shipping Shipping { get; set; }


        /// <summary>
        /// the payment methods
        /// </summary>
        /// <value>The payment methods.</value>
        [XmlElement(ElementName = "payment-methods")]
        [JsonProperty(PropertyName = "payment-methods")]
        public PaymentMethods PaymentMethods { get; set; }

        /// <summary>
        /// the transaction type / must be set in config or can be set in <see cref="PaymentInfo"></see>
        /// </summary>
        /// <value>The type of the transaction.</value>
        [XmlElement(ElementName = "transaction-type")]
        [JsonProperty(PropertyName = "transaction-type")]
        public string TransactionType { get; set; }

        /// <summary>
        /// the card token for getting stored card
        /// </summary>
        /// <value>The card token.</value>
        [XmlElement(ElementName = "card-token")]
        [JsonProperty(PropertyName = "card-token")]
        public CardToken CardToken { get; set; }

        /// <summary>
        /// the app id
        /// </summary>
        /// <value>The API identifier.</value>
        [XmlElement(ElementName = "api-id")]
        [JsonProperty(PropertyName = "api-id")]
        public string ApiId { get; set; }

        /// <summary>
        /// the device
        /// </summary>
        /// <value>The device.</value>
        [JsonProperty(PropertyName = "device")]
        public Device Device { get; set; }

        /// <summary>
        /// the descriptor
        /// </summary>
        /// <value>The descriptor.</value>
        [JsonProperty(PropertyName = "descriptor")]
        public string Descriptor { get; set; }

        /// <summary>
        /// Gets or sets the notifications.
        /// </summary>
        /// <value>The notifications.</value>
        [JsonProperty(PropertyName = "notifications")]
        public Notifications Notifications { get; set; }


        [JsonProperty(PropertyName = "returnurl")]
        [XmlIgnore]
        public string ReturnUrl { get; set; }
    }
}