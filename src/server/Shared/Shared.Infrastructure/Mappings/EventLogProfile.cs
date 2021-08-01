using AutoMapper;
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
        }
    }
}