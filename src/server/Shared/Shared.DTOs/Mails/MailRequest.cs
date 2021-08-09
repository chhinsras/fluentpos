// --------------------------------------------------------------------------------------------------
// <copyright file="MailRequest.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

namespace FluentPOS.Shared.DTOs.Mails
{
    public class MailRequest
    {
        public string To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string From { get; set; }
    }
}