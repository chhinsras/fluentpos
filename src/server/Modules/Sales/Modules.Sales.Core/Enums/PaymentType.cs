// --------------------------------------------------------------------------------------------------
// <copyright file="PaymentType.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

namespace FluentPOS.Modules.Sales.Core.Enums
{
    public enum PaymentType : byte
    {
        Cash,

        CreditCard,

        GiftCard
    }
}