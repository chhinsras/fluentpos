// --------------------------------------------------------------------------------------------------
// <copyright file="IEventLogService.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentPOS.Shared.Core.EventLogging;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Identity.EventLogs;

namespace FluentPOS.Shared.Core.Interfaces.Services
{
    public interface IEventLogService
    {
        Task<PaginatedResult<EventLog>> GetAllAsync(GetEventLogsRequest request);

        Task<Result<string>> LogCustomEventAsync(LogEventRequest request);
    }
}