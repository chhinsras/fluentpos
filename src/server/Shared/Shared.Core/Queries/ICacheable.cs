// --------------------------------------------------------------------------------------------------
// <copyright file="ICacheable.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;

namespace FluentPOS.Shared.Core.Queries
{
    public interface ICacheable
    {
        bool BypassCache { get; }

        string CacheKey { get; }

        TimeSpan? SlidingExpiration { get; }
    }
}