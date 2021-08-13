// --------------------------------------------------------------------------------------------------
// <copyright file="RemoveExtendedAttributeCommand.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Shared.Core.Contracts;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;

namespace FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands
{
    // ReSharper disable once UnusedTypeParameter
    public class RemoveExtendedAttributeCommand<TEntityId, TEntity> : IRequest<Result<Guid>>
        where TEntity : class, IEntity<TEntityId>
    {
        public Guid Id { get; }

        public RemoveExtendedAttributeCommand(Guid entityExtendedAttributeId)
        {
            Id = entityExtendedAttributeId;
        }
    }
}