// --------------------------------------------------------------------------------------------------
// <copyright file="EventLogProfile.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using AutoMapper;
using FluentPOS.Shared.Core.EventLogging;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.DTOs.Identity.EventLogs;

namespace FluentPOS.Shared.Infrastructure.Mappings
{
    public class EventLogProfile : Profile
    {
        public EventLogProfile()
        {
            CreateMap<PaginatedEventLogsFilter, GetEventLogsRequest>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
            CreateMap<LogEventRequest, EventLog>().ReverseMap();
        }
    }
}