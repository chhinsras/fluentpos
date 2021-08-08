// <copyright file="IEntity.cs" company="Fluentpos">
// --------------------------------------------------------------------------------------------------
// Copyright (c) Fluentpos. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// --------------------------------------------------------------------------------------------------
// </copyright>

namespace FluentPOS.Shared.Core.Contracts
{
    public interface IEntity<TEntityId> : IEntity
    {
        public TEntityId Id { get; set; }
    }

    public interface IEntity
    {
    }
}