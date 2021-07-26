using AutoMapper;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Modules.People.Core.Features.Carts.Commands;
using FluentPOS.Shared.DTOs.People.Carts;

namespace FluentPOS.Modules.People.Core.Mappings
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<CreateCartCommand, Cart>().ReverseMap();
            CreateMap<GetCartByIdResponse, Cart>().ReverseMap();
            CreateMap<GetAllPagedCartsResponse, Cart>().ReverseMap();
        }
    }
}