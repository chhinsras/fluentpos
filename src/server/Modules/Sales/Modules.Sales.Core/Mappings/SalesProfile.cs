using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentPOS.Modules.Sales.Core.Entities;
using FluentPOS.Modules.Sales.Core.Features.Sales.Queries;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.DTOs.Sales.Orders;

namespace FluentPOS.Modules.Sales.Core.Mappings
{
     public class SalesProfile : Profile
    {
        public SalesProfile()
        {
            CreateMap<GetSalesResponse, Order>().ReverseMap();
            CreateMap<PaginatedSalesFilter, GetSalesQuery>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
        }
    }
}