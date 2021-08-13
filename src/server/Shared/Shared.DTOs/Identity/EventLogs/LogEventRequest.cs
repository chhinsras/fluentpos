// --------------------------------------------------------------------------------------------------
// <copyright file="LogEventRequest.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;

namespace FluentPOS.Shared.DTOs.Identity.EventLogs
{
    public class LogEventRequest
    {
        public LogEventRequest()
        {
        }

        public string Event { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }

        public Guid UserId { get; set; }
    }
}