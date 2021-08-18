// --------------------------------------------------------------------------------------------------
// <copyright file="EntityReference.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;

namespace FluentPOS.Shared.Core.Entities
{
    public class EntityReference
    {
        public EntityReference(string entity)
        {
            Entity = entity;
            MonthYearString = DateTime.Now.ToString("MMyy");
            LastUpdateOn = DateTime.Now;
            Count = 1;
        }

        public void Increment()
        {
            LastUpdateOn = DateTime.Now;
            Count++;
        }

        public int Id { get; private set; }

        public string Entity { get; private set; }

        public string MonthYearString { get; private set; }

        public int Count { get; private set; }

        public DateTime LastUpdateOn { get; private set; }
    }
}