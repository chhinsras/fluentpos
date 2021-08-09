// --------------------------------------------------------------------------------------------------
// <copyright file="SmsSettings.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

namespace FluentPOS.Shared.Core.Settings
{
    public class SmsSettings
    {
        public string SmsAccountIdentification { get; set; }

        public string SmsAccountPassword { get; set; }

        public string SmsAccountFrom { get; set; }

        public bool EnableVerification { get; set; }
    }
}