using AutoMapper;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Modules.People.Core.Features.Customers.Commands;
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
            CreateMap<GetAllPagedCustomersResponse, Customer>().ReverseMap();
        }
    }
}