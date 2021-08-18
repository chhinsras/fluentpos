// --------------------------------------------------------------------------------------------------
// <copyright file="StockService.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentPOS.Modules.Inventory.Core.Abstractions;
using FluentPOS.Modules.Inventory.Core.Entities;
using FluentPOS.Modules.Inventory.Core.Enums;
using FluentPOS.Modules.Inventory.Core.Exceptions;
using FluentPOS.Shared.Core.IntegrationServices.Inventory;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Inventory.Infrastructure.Services
{
    /// <summary>
    /// Integration Services for Inventory Module.
    /// </summary>
    public class StockService : IStockService
    {
        private readonly IInventoryDbContext _context;
        private readonly IStringLocalizer<StockService> _localizer;

        /// <summary>
        /// Stock Service.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <param name="localizer">Localizer.</param>
        public StockService(
            IInventoryDbContext context,
            IStringLocalizer<StockService> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        /// <summary>
        /// Record Transaction.
        /// </summary>
        /// <param name="productId">Product Id.</param>
        /// <param name="quantity">Quantity.</param>
        /// <param name="referenceNumber">Reference Number.</param>
        /// <param name="isSale">Is Sale.</param>
        /// <returns>Task Completed.</returns>
        public async Task RecordTransaction(Guid productId, decimal quantity, string referenceNumber, bool isSale = true)
        {
            // TODO - Move this to MediatR, maybe? - Important, DO NOT make an API endpoint for this.

            var transactionType = isSale ? TransactionType.Sale : TransactionType.Purchase;
            var stockTransaction = new StockTransaction(productId, quantity, transactionType, referenceNumber);
            await _context.StockTransactions.AddAsync(stockTransaction);

            bool hasStockRecord = _context.Stocks.Any(a => a.ProductId == productId);
            if (hasStockRecord)
            {
                if (isSale)
                {
                    var stockRecord = _context.Stocks.FirstOrDefault(s => s.ProductId == productId);
                    if (stockRecord == null)
                    {
                        throw new InventoryException(_localizer["Stock Record Not Found"], HttpStatusCode.NotFound);
                    }

                    stockRecord.ReduceQuantity(quantity);
                    _context.Stocks.Update(stockRecord);
                }
                else
                {
                    var stockRecord = _context.Stocks.FirstOrDefault(s => s.ProductId == productId);
                    if (stockRecord == null)
                    {
                        throw new InventoryException(_localizer["Stock Record Not Found"], HttpStatusCode.NotFound);
                    }

                    stockRecord.IncreaseQuantity(quantity);
                    _context.Stocks.Update(stockRecord);
                }
            }
            else
            {
                var stockRecord = new Stock(productId);
                if (isSale)
                {
                    stockRecord.ReduceQuantity(quantity);
                }
                else
                {
                    stockRecord.IncreaseQuantity(quantity);
                }

                _context.Stocks.Add(stockRecord);
            }

            await _context.SaveChangesAsync();
        }
    }
}