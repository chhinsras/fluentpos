using AutoMapper;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Modules.People.Core.Features.Customers.Commands;
using FluentPOS.Modules.People.Core.Features.Customers.Queries;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.DTOs.People.Customers;

namespace FluentPOS.Modules.People.Core.Mappings
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<RegisterCustomerCommand, Customer>().ReverseMap();
            CreateMap<UpdateCustomerCommand, Customer>().ReverseMap();
            CreateMap<GetCustomerByIdResponse, Customer>().ReverseMap();
            CreateMap<GetCustomersResponse, Customer>().ReverseMap();
            CreateMap<PaginatedCustomerFilter, GetCustomersQuery>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
        }
    }
}