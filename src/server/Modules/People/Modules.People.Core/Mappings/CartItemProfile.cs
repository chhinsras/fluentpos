using AutoMapper;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Modules.People.Core.Features.CartItems.Commands;
using FluentPOS.Shared.DTOs.People.CartItems;

namespace FluentPOS.Modules.People.Core.Mappings
{
    public class CartItemProfile : Profile
    {
        public CartItemProfile()
        {
            CreateMap<AddCartItemCommand, CartItem>().ReverseMap();
            CreateMap<UpdateCartItemCommand, CartItem>().ReverseMap();
            CreateMap<GetAllPagedCartItemsResponse, CartItem>().ReverseMap();
            CreateMap<GetCartItemByIdResponse, CartItem>().ReverseMap();
        }
    }
}