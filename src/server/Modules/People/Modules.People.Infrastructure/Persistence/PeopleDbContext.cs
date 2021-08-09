// --------------------------------------------------------------------------------------------------
// <copyright file="PeopleDbContext.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Modules.People.Core.Abstractions;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Modules.People.Core.Entities.ExtendedAttributes;
using FluentPOS.Modules.People.Infrastructure.Extensions;
using FluentPOS.Shared.Core.EventLogging;
using FluentPOS.Shared.Core.Interfaces;
using FluentPOS.Shared.Core.Interfaces.Serialization;
using FluentPOS.Shared.Core.Settings;
using FluentPOS.Shared.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FluentPOS.Modules.People.Infrastructure.Persistence
{
    public sealed class PeopleDbContext : ModuleDbContext, IPeopleDbContext,
        IExtendedAttributeDbContext<Guid, Customer, CustomerExtendedAttribute>,
        IExtendedAttributeDbContext<Guid, Cart, CartExtendedAttribute>,
        IExtendedAttributeDbContext<Guid, CartItem, CartItemExtendedAttribute>
    {
        private readonly PersistenceSettings _persistenceOptions;
        private readonly IJsonSerializer _json;

        protected override string Schema => "People";

        public PeopleDbContext(
            DbContextOptions<PeopleDbContext> options,
            IMediator mediator,
            IEventLogger eventLogger,
            IOptions<PersistenceSettings> persistenceOptions,
            IJsonSerializer json)
                : base(options, mediator, eventLogger, persistenceOptions, json)
        {
            _persistenceOptions = persistenceOptions.Value;
            _json = json;
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyPeopleConfiguration(_persistenceOptions);
        }

        DbSet<Customer> IExtendedAttributeDbContext<Guid, Customer, CustomerExtendedAttribute>.GetEntities() => Customers;

        DbSet<CustomerExtendedAttribute> IExtendedAttributeDbContext<Guid, Customer, CustomerExtendedAttribute>.ExtendedAttributes { get; set; }

        DbSet<Cart> IExtendedAttributeDbContext<Guid, Cart, CartExtendedAttribute>.GetEntities() => Carts;

        DbSet<CartExtendedAttribute> IExtendedAttributeDbContext<Guid, Cart, CartExtendedAttribute>.ExtendedAttributes { get; set; }

        DbSet<CartItem> IExtendedAttributeDbContext<Guid, CartItem, CartItemExtendedAttribute>.GetEntities() => CartItems;

        DbSet<CartItemExtendedAttribute> IExtendedAttributeDbContext<Guid, CartItem, CartItemExtendedAttribute>.ExtendedAttributes { get; set; }
    }
}