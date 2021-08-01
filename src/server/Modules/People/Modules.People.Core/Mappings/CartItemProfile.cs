using AutoMapper;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Modules.People.Core.Features.CartItems.Commands;
using FluentPOS.Modules.People.Core.Features.CartItems.Queries;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.DTOs.People.CartItems;

namespace FluentPOS.Modules.People.Core.Mappings
{
    public class CartItemProfile : Profile
    {
        public CartItemProfile()
        {
            CreateMap<AddCartItemCommand, CartItem>().ReverseMap();
            CreateMap<UpdateCartItemCommand, CartItem>().ReverseMap();

            CreateMap<GetCartItemByIdResponse, CartItem>().ReverseMap();
            CreateMap<PaginatedCartItemFilter, GetCartItemsQuery>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
        }
    }
}