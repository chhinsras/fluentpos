// --------------------------------------------------------------------------------------------------
// <copyright file="UserRolesRequest.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace FluentPOS.Shared.DTOs.Identity.Users
{
    public class UserRolesRequest
    {
        public List<UserRoleModel> UserRoles { get; set; } = new();
    }
}