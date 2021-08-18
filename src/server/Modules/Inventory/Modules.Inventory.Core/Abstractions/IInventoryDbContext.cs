using FluentPOS.Modules.Inventory.Core.Entities;
using Microsoft.EntityFrameworkCore;
using FluentPOS.Shared.Core.Interfaces;

namespace FluentPOS.Modules.Inventory.Core.Abstractions
{
    public interface IInventoryDbContext : IDbContext
    {
        public DbSet<Stock> Stocks { get; set; }

        public DbSet<StockTransaction> StockTransactions { get; set; }
    }
}