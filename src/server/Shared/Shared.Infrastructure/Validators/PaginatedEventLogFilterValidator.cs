// --------------------------------------------------------------------------------------------------
// <copyright file="PaginatedEventLogFilterValidator.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Shared.Core.EventLogging;
using FluentPOS.Shared.Core.Features.Common.Queries.Validators;
using FluentPOS.Shared.DTOs.Identity.EventLogs;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Shared.Infrastructure.Validators
{
    public class PaginatedEventLogFilterValidator : PaginatedFilterValidator<Guid, EventLog, PaginatedEventLogsFilter>
    {
        public PaginatedEventLogFilterValidator(IStringLocalizer<PaginatedEventLogFilterValidator> localizer)
            : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}