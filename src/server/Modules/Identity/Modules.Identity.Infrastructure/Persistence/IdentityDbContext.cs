using System.Threading;
using System.Threading.Tasks;
using FluentPOS.Modules.Identity.Core.Abstractions;
using FluentPOS.Modules.Identity.Core.Entities;
using FluentPOS.Modules.Identity.Infrastructure.Extensions;
using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.Core.EventLogging;
using FluentPOS.Shared.Core.Interfaces;
using FluentPOS.Shared.Core.Settings;
using FluentPOS.Shared.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FluentPOS.Modules.Identity.Infrastructure.Persistence
{
    public sealed class IdentityDbContext : IdentityDbContext<FluentUser, FluentRole, string, IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>, FluentRoleClaim, IdentityUserToken<string>>,
        IIdentityDbContext,
        IModuleDbContext,
        IExtendedAttributeDbContext<string, FluentUser, UserExtendedAttribute>,
        IExtendedAttributeDbContext<string, FluentRole, RoleExtendedAttribute>
    {
        private readonly IMediator _mediator;
        private readonly IEventLogger _eventLogger;
        private readonly PersistenceSettings _persistenceOptions;

        internal string Schema => "Identity";

        public IdentityDbContext(
            DbContextOptions<IdentityDbContext> options,
            IOptions<PersistenceSettings> persistenceOptions,
            IMediator mediator,
            IEventLogger eventLogger)
                : base(options)
        {
            _mediator = mediator;
            _eventLogger = eventLogger;
            _persistenceOptions = persistenceOptions.Value;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);
            modelBuilder.Ignore<Event>();
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            modelBuilder.ApplyIdentityConfiguration(_persistenceOptions);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await this.SaveChangeWithPublishEventsAsync(_eventLogger, _mediator, cancellationToken);
        }

        public override int SaveChanges()
        {
            return this.SaveChangeWithPublishEvents(_eventLogger, _mediator);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return SaveChanges();
        }

        DbSet<FluentUser> IExtendedAttributeDbContext<string, FluentUser, UserExtendedAttribute>.GetEntities() => Users;

        DbSet<UserExtendedAttribute> IExtendedAttributeDbContext<string, FluentUser, UserExtendedAttribute>.ExtendedAttributes { get; set; }

        DbSet<FluentRole> IExtendedAttributeDbContext<string, FluentRole, RoleExtendedAttribute>.GetEntities() => Roles;

        DbSet<RoleExtendedAttribute> IExtendedAttributeDbContext<string, FluentRole, RoleExtendedAttribute>.ExtendedAttributes { get; set; }
    }
}