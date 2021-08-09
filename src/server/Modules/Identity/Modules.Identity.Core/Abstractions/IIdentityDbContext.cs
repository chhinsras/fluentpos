// --------------------------------------------------------------------------------------------------
// <copyright file="IIdentityDbContext.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Modules.Identity.Core.Entities;
using FluentPOS.Shared.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FluentPOS.Modules.Identity.Core.Abstractions
{
    public interface IIdentityDbContext : IDbContext
    {
        public DbSet<FluentUser> Users { get; set; }

        public DbSet<FluentRole> Roles { get; set; }

        public DbSet<FluentRoleClaim> RoleClaims { get; set; }
    }
}