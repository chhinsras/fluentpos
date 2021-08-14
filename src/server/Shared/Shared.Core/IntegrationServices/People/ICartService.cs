// --------------------------------------------------------------------------------------------------
// <copyright file="ICartService.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.People.Carts;

namespace FluentPOS.Shared.Core.IntegrationServices.People
{
    public interface ICartService
    {
        Task<Result<GetCartByIdResponse>> GetDetailsAsync(Guid cartId);

        Task<Result<Guid>> RemoveCartAsync(Guid cartId);
    }
}