// --------------------------------------------------------------------------------------------------
// <copyright file="StockTransaction.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Modules.Inventory.Core.Enums;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Inventory.Core.Entities
{
    public class StockTransaction : BaseEntity
    {
        public StockTransaction(Guid productId, decimal quantity, TransactionType type, string referenceNumber)
        {
            ProductId = productId;
            Quantity = quantity;
            Type = type;
            ReferenceNumber = referenceNumber;
            Timestamp = DateTime.Now;
        }

        public Guid ProductId { get; private set; }

        public DateTime Timestamp { get; private set; }

        public decimal Quantity { get; private set; }

        public TransactionType Type { get; private set; }

        public string ReferenceNumber { get; private set; }
    }
}