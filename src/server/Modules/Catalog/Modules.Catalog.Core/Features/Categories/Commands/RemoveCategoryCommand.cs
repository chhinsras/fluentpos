// --------------------------------------------------------------------------------------------------
// <copyright file="RemoveCategoryCommand.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;

namespace FluentPOS.Modules.Catalog.Core.Features.Categories.Commands
{
    public class RemoveCategoryCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; }

        public RemoveCategoryCommand(Guid categoryId)
        {
            Id = categoryId;
        }
    }
}