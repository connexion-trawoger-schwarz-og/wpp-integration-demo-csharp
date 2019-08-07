// ***********************************************************************
// Assembly         : DemoWebsite
// Author           : r.wienicke
// Created          : 08-01-2019
//
// Last Modified By : r.wienicke
// Last Modified On : 08-01-2019
// ***********************************************************************
// <copyright file="TransactionType.cs" company="connexion e.solutions">
//     Copyright (c) connexion e.solutions. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Wirecard.Models
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