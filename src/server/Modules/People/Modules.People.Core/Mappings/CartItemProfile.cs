// --------------------------------------------------------------------------------------------------
// <copyright file="CartItemProfile.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using AutoMapper;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Modules.People.Core.Features.CartItems.Commands;
using FluentPOS.Modules.People.Core.Features.CartItems.Queries;
using FluentPOS.Shared.Core.Features.Common.Filters;
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
            CreateMap<GetByIdCacheableFilter<Guid, CartItem>, GetCartItemByIdQuery>();
            CreateMap<GetCartItemByIdResponse, CartItem>().ReverseMap();
            CreateMap<PaginatedCartItemFilter, GetCartItemsQuery>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
        }
    }
}