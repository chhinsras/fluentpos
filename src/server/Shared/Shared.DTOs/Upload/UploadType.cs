// --------------------------------------------------------------------------------------------------
// <copyright file="UploadType.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.ComponentModel;

namespace FluentPOS.Shared.DTOs.Upload
{
    public enum UploadType
    {
        [Description(@"Images\Catalog\Products")]
        Product,

        [Description(@"Images\Catalog\Brands")]
        Brand,

        [Description(@"Images\Catalog\Categories")]
        Category,

        [Description(@"Images\People\Customers")]
        Customer
    }
}