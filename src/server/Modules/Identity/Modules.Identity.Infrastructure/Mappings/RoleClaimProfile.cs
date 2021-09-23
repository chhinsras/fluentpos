// --------------------------------------------------------------------------------------------------
// <copyright file="RoleClaimProfile.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using AutoMapper;
using FluentPOS.Modules.Identity.Core.Entities;
using FluentPOS.Shared.DTOs.Identity.Roles;

namespace FluentPOS.Modules.Identity.Infrastructure.Mappings
{
    public class RoleClaimProfile : Profile
    {
        public RoleClaimProfile()
        {
            CreateMap<RoleClaimResponse, FluentRoleClaim>()
                .ForMember(nameof(FluentRoleClaim.ClaimType), opt => opt.MapFrom(c => c.Type))
                .ForMember(nameof(FluentRoleClaim.ClaimValue), opt => opt.MapFrom(c => c.Value))
                .ReverseMap();

            CreateMap<RoleClaimRequest, FluentRoleClaim>()
                .ForMember(nameof(FluentRoleClaim.ClaimType), opt => opt.MapFrom(c => c.Type))
                .ForMember(nameof(FluentRoleClaim.ClaimValue), opt => opt.MapFrom(c => c.Value))
                .ReverseMap();

            CreateMap<RoleClaimModel, RoleClaimRequest>();
            CreateMap<RoleClaimModel, FluentRoleClaim>().ReverseMap();
            CreateMap<RoleClaimModel, RoleClaimResponse>().ReverseMap();
        }
    }
}