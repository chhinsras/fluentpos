// --------------------------------------------------------------------------------------------------
// <copyright file="Brand.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using FluentPOS.Modules.Catalog.Core.Entities.ExtendedAttributes;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Catalog.Core.Entities
{
    public class Brand : BaseEntity
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Detail { get; set; }

        public virtual ICollection<BrandExtendedAttribute> ExtendedAttributes { get; set; }

        public Brand()
            : base()
        {
            ExtendedAttributes = new HashSet<BrandExtendedAttribute>();
        }
    }
}