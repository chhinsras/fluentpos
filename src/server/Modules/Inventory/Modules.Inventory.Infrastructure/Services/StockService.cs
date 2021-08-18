// --------------------------------------------------------------------------------------------------
// <copyright file="StockService.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using FluentPOS.Modules.Inventory.Core.Abstractions;
using FluentPOS.Modules.Inventory.Core.Entities;
using FluentPOS.Modules.Inventory.Core.Enums;
using FluentPOS.Shared.Core.IntegrationServices.Inventory;

namespace FluentPOS.Modules.Inventory.Infrastructure.Services
{
    /// <inheritdoc/>
    public class StockService : IStockService
    {
        private readonly IInventoryDbContext _context;

        /// <summary>
        /// Stock Service.
        /// </summary>
        /// <param name="context">Context.</param>
        public StockService(
            IInventoryDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task RecordTransaction(Guid productId, decimal quantity, string referenceNumber, bool isSale = true)
        {
            // TODO - Move this to MediatR, maybe? - Important, DO NOT make an API endpoint for this.

            var transactionType = isSale ? TransactionType.Sale : TransactionType.Purchase;
            var stockTransaction = new StockTransaction(productId, quantity, transactionType, referenceNumber);
            await _context.StockTransactions.AddAsync(stockTransaction);

            var stockRecord = _context.Stocks.FirstOrDefault(s => s.ProductId == productId);
            switch (transactionType)
            {
                case TransactionType.Sale:
                    if (stockRecord != null)
                    {
                        stockRecord.ReduceQuantity(quantity);
                        _context.Stocks.Update(stockRecord);
                    }
                    else
                    {
                        stockRecord = new Stock(productId);
                        stockRecord.ReduceQuantity(quantity);
                        _context.Stocks.Add(stockRecord);
                    }

                    break;
                case TransactionType.Purchase:
                    if (stockRecord != null)
                    {
                        stockRecord.IncreaseQuantity(quantity);
                        _context.Stocks.Update(stockRecord);
                    }
                    else
                    {
                        stockRecord = new Stock(productId);
                        stockRecord.IncreaseQuantity(quantity);
                        _context.Stocks.Add(stockRecord);
                    }

                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(transactionType), transactionType, null);
            }

            await _context.SaveChangesAsync();
        }
    }
}