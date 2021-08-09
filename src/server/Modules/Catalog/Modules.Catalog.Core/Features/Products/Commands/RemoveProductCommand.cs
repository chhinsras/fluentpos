// --------------------------------------------------------------------------------------------------
// <copyright file="RemoveProductCommand.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;

namespace FluentPOS.Modules.Catalog.Core.Features.Products.Commands
{
    public class RemoveProductCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }

        public RemoveProductCommand(Guid productId)
        {
            Id = productId;
        }
    }
}