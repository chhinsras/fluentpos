using FluentPOS.Shared.Core.Contracts;
using FluentPOS.Shared.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluentPOS.Shared.Core.Interfaces
{
    public interface IExtendedAttributeDbContext<TEntityId, TEntity, TExtendedAttribute> : IDbContext
        where TExtendedAttribute : ExtendedAttribute<TEntityId, TEntity>
        where TEntity : class, IEntity<TEntityId>
    {
        [NotMapped]
        public DbSet<TEntity> Entities => GetEntities();

        protected DbSet<TEntity> GetEntities();

        public DbSet<TExtendedAttribute> ExtendedAttributes { get; set; }
    }
}