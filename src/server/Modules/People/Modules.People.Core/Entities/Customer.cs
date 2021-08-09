// --------------------------------------------------------------------------------------------------
// <copyright file="Customer.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using FluentPOS.Modules.People.Core.Entities.ExtendedAttributes;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.People.Core.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string ImageUrl { get; set; }

        public string Type { get; set; }

        public virtual ICollection<CustomerExtendedAttribute> ExtendedAttributes { get; set; }

        public Customer()
            : base()
        {
            ExtendedAttributes = new HashSet<CustomerExtendedAttribute>();
        }
    }
}