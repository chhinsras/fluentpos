// --------------------------------------------------------------------------------------------------
// <copyright file="SmsRequest.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

namespace FluentPOS.Shared.DTOs.Sms
{
    public class SmsRequest
    {
        public string Number { get; set; }

        public string Message { get; set; }
    }
}