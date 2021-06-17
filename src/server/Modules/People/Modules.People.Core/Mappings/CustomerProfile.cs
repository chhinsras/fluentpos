using AutoMapper;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Modules.People.Core.Features.Customers.Commands;

namespace FluentPOS.Modules.People.Core.Mappings
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<RegisterCustomerCommand, Customer>().ReverseMap();
            CreateMap<UpdateCustomerCommand, Customer>().ReverseMap();
        }
    }
}