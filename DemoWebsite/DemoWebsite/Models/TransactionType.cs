// Copyright (c) 2019 connexion OG / Roman Wienicke
namespace DemoWebsite.Models
{
    /// <summary>
    /// the payment transaction type
    /// </summary>
    public enum TransactionType
    {
        /// <summary>
        /// payment is autodized but not charged
        /// </summary>
        Authorization,
        /// <summary>
        /// debit form the account 
        /// </summary>
        Debit,
        /// <summary>
        /// order is charged from the account
        /// </summary>
        Order

    }
       

   
}