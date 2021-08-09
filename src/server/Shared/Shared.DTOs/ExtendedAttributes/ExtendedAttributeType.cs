// --------------------------------------------------------------------------------------------------
// <copyright file="ExtendedAttributeType.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

namespace FluentPOS.Shared.DTOs.ExtendedAttributes
{
    public enum ExtendedAttributeType : byte
    {
        Decimal = 1,

        Text = 2,

        DateTime = 3,

        Json = 4,

        Boolean = 5,

        Integer = 6
    }
}