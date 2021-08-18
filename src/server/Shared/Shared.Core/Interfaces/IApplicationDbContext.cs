// --------------------------------------------------------------------------------------------------
// <copyright file="IApplicationDbContext.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Shared.Core.Entities;
using FluentPOS.Shared.Core.EventLogging;
using Microsoft.EntityFrameworkCore;

namespace FluentPOS.Shared.Core.Interfaces
{
    public interface IApplicationDbContext : IDbContext
    {
        public DbSet<EventLog> EventLogs { get; set; }

        public DbSet<EntityReference> EntityReferences { get; set; }
    }
}