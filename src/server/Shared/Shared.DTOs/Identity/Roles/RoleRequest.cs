// --------------------------------------------------------------------------------------------------
// <copyright file="RoleRequest.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

namespace FluentPOS.Shared.DTOs.Identity.Roles
{
    public class RoleRequest
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}