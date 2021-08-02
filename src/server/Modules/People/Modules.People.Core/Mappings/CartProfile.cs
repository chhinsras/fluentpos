using AutoMapper;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Modules.People.Core.Features.Carts.Commands;
using FluentPOS.Modules.People.Core.Features.Carts.Queries;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.DTOs.People.Carts;

namespace FluentPOS.Modules.People.Core.Mappings
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<CreateCartCommand, Cart>().ReverseMap();
            CreateMap<GetCartByIdResponse, Cart>().ReverseMap();
            CreateMap<GetCartsResponse, Cart>().ReverseMap();
            CreateMap<PaginatedCartFilter, GetCartsQuery>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
        }
    }
}