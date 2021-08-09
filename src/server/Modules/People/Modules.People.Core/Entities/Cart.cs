// --------------------------------------------------------------------------------------------------
// <copyright file="Cart.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using FluentPOS.Modules.People.Core.Entities.ExtendedAttributes;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.People.Core.Entities
{
    public class Cart : BaseEntity
    {
        public Guid CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public DateTime Timestamp { get; private set; }

        public virtual ICollection<CartItem> CartItems { get; set; }

        public virtual ICollection<CartExtendedAttribute> ExtendedAttributes { get; set; }

        public Cart()
        {
            CartItems = new List<CartItem>();
            ExtendedAttributes = new HashSet<CartExtendedAttribute>();
            Timestamp = DateTime.Now;
        }
    }
}