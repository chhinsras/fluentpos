using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace FluentPOS.Shared.Core.Interfaces
{
    public interface IExtendedAttributeDbContext<TEntity, TExtendedAttribute>
        where TExtendedAttribute : ExtendedAttribute<TEntity>
        where TEntity : BaseEntity
    {
        [NotMapped]
        public DbSet<TEntity> Entities => GetEntities();
        protected DbSet<TEntity> GetEntities();
        public DbSet<TExtendedAttribute> ExtendedAttributes { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}