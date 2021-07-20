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