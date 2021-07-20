using System.Threading;
using System.Threading.Tasks;
using FluentPOS.Modules.Identity.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FluentPOS.Modules.Identity.Core.Abstractions
{
    public interface IIdentityDbContext
    {
        public DbSet<FluentUser> Users { get; set; }

        public DbSet<FluentRole> Roles { get; set; }

        public DbSet<FluentRoleClaim> RoleClaims { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());

        int SaveChanges();
    }
}