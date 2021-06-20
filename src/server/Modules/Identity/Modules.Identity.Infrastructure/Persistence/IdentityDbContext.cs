using FluentPOS.Modules.Identity.Core.Entities;
using FluentPOS.Modules.Identity.Infrastructure.Extensions;
using FluentPOS.Shared.Core.Settings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

namespace FluentPOS.Modules.Identity.Infrastructure.Persistence
{
    public class IdentityDbContext : IdentityDbContext<ExtendedIdentityUser, ExtendedIdentityRole, string>
    {
        private readonly PersistenceSettings _persistenceOptions;

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options, IOptions<PersistenceSettings> persistenceOptions) : base(options)
        {
            _persistenceOptions = persistenceOptions.Value;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            modelBuilder.ApplyIdentityConfiguration(_persistenceOptions);
        }
    }
}