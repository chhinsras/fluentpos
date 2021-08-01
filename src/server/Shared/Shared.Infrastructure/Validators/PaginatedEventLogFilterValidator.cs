using System;
using FluentPOS.Shared.Core.EventLogging;
using FluentPOS.Shared.Core.Features.Common.Queries.Validators;
using FluentPOS.Shared.DTOs.Identity.EventLogs;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Shared.Infrastructure.Validators
{
    public class PaginatedEventLogFilterValidator : PaginatedFilterValidator<Guid, EventLog, PaginatedEventLogsFilter>
    {
        public PaginatedEventLogFilterValidator(IStringLocalizer<PaginatedEventLogFilterValidator> localizer) : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}