using FluentPOS.Modules.People.Core.Abstractions;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Modules.People.Infrastructure.Extensions;
using FluentPOS.Shared.Core.EventLogging;
using FluentPOS.Shared.Core.Interfaces;
using FluentPOS.Shared.Core.Settings;
using FluentPOS.Shared.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using FluentPOS.Modules.People.Core.Entities.ExtendedAttributes;

namespace FluentPOS.Modules.People.Infrastructure.Persistence
{
    public sealed class PeopleDbContext : ModuleDbContext, IPeopleDbContext,
        IExtendedAttributeDbContext<Guid, Customer, CustomerExtendedAttribute>
    {
        private readonly PersistenceSettings _persistenceOptions;

        protected override string Schema => "People";

        public PeopleDbContext(
            DbContextOptions<PeopleDbContext> options,
            IMediator mediator,
            IEventLogger eventLogger,
            IOptions<PersistenceSettings> persistenceOptions)
                : base(options, mediator, eventLogger, persistenceOptions)
        {
            _persistenceOptions = persistenceOptions.Value;
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
    }
}