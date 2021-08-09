// --------------------------------------------------------------------------------------------------
// <copyright file="Category.cs" company="FluentPOS">
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
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Detail { get; set; }

        public virtual ICollection<CategoryExtendedAttribute> ExtendedAttributes { get; set; }

        public Category()
            : base()
        {
            ExtendedAttributes = new HashSet<CategoryExtendedAttribute>();
        }
    }
}