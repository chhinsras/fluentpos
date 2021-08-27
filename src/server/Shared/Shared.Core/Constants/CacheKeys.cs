// --------------------------------------------------------------------------------------------------
// <copyright file="CacheKeys.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Shared.Core.Contracts;
using FluentPOS.Shared.Core.Utilities;

namespace FluentPOS.Shared.Core.Constants
{
    public static class CacheKeys
    {
        public static class Common
        {
            public static string GetEntityByIdCacheKey<TEntityId, TEntity>(TEntityId id)
                where TEntity : class, IEntity<TEntityId>
            {
                return $"GetEntity-{typeof(TEntity).GetGenericTypeName()}-{id}";
            }
        }
    }
}