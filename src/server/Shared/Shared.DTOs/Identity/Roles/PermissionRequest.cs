// --------------------------------------------------------------------------------------------------
// <copyright file="PermissionRequest.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace FluentPOS.Shared.DTOs.Identity.Roles
{
    public class PermissionRequest
    {
        public string RoleId { get; set; }

        public IList<RoleClaimModel> RoleClaims { get; set; }
    }
}