using FluentPOS.Modules.People.Core.Entities;
using Microsoft.EntityFrameworkCore;
using FluentPOS.Shared.Core.Interfaces;

namespace FluentPOS.Modules.People.Core.Abstractions
{
    public interface IPeopleDbContext : IDbContext
    {
        public DbSet<Customer> Customers { get; set; }
    }
}