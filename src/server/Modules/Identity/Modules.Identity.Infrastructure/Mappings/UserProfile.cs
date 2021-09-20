// --------------------------------------------------------------------------------------------------
// <copyright file="UserProfile.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using AutoMapper;
using FluentPOS.Modules.Identity.Core.Entities;
using FluentPOS.Shared.DTOs.Identity.Users;

namespace FluentPOS.Modules.Identity.Infrastructure.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserResponse, FluentUser>().ReverseMap();
            CreateMap<UpdateUserRequest, FluentUser>().ReverseMap();
        }
    }
}