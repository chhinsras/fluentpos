// --------------------------------------------------------------------------------------------------
// <copyright file="EventLogService.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FluentPOS.Shared.Core.EventLogging;
using FluentPOS.Shared.Core.Exceptions;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.Interfaces;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Identity.EventLogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Shared.Infrastructure.Services
{
    public class EventLogService : IEventLogService
    {
        private readonly IEventLogger _logger;
        private readonly IApplicationDbContext _dbContext;
        private readonly IStringLocalizer<EventLogService> _localizer;
        private readonly IMapper _mapper;

        public EventLogService(
            IApplicationDbContext dbContext,
            IStringLocalizer<EventLogService> localizer,
            IMapper mapper,
            IEventLogger logger)
        {
            _dbContext = dbContext;
            _localizer = localizer;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PaginatedResult<EventLog>> GetAllAsync(GetEventLogsRequest request)
        {
            var queryable = _dbContext.EventLogs.AsNoTracking().AsQueryable();

            if (request.UserId != Guid.Empty)
            {
                queryable = queryable.Where(x => x.UserId.Equals(request.UserId));
            }

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                queryable = queryable.Where(x => EF.Functions.Like(x.Email.ToLower(), $"%{request.Email.ToLower()}%"));
            }

            if (!string.IsNullOrWhiteSpace(request.MessageType))
            {
                queryable = queryable.Where(x => EF.Functions.Like(x.MessageType.ToLower(), $"%{request.MessageType.ToLower()}%"));
            }

            string ordering = new OrderByConverter().Convert(request.OrderBy);
            queryable = !string.IsNullOrWhiteSpace(ordering) ? queryable.OrderBy(ordering) : queryable.OrderByDescending(a => a.Timestamp);

            if (!string.IsNullOrEmpty(request.SearchString))
            {
                string lowerSearchString = request.SearchString.ToLower();
                queryable = queryable.Where(x => (!string.IsNullOrWhiteSpace(x.Data) && EF.Functions.Like(x.Data.ToLower(), $"%{lowerSearchString}%"))
                                                 || (!string.IsNullOrWhiteSpace(x.OldValues) && EF.Functions.Like(x.OldValues.ToLower(), $"%{lowerSearchString}%"))
                                                 || (!string.IsNullOrWhiteSpace(x.NewValues) && EF.Functions.Like(x.NewValues.ToLower(), $"%{lowerSearchString}%"))
                                                 || EF.Functions.Like(x.Id.ToString().ToLower(), $"%{lowerSearchString}%"));
            }

            var eventLogList = await queryable
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            if (eventLogList == null)
            {
                throw new CustomException(_localizer["Event Logs Not Found!"], statusCode: HttpStatusCode.NotFound);
            }

            return eventLogList;
        }

        public async Task<Result<string>> LogCustomEventAsync(LogEventRequest request)
        {
            var log = _mapper.Map<EventLog>(request);
            await _logger.SaveAsync(log, default);
            return await Result<string>.SuccessAsync(data: log.Id.ToString());
        }
    }
}