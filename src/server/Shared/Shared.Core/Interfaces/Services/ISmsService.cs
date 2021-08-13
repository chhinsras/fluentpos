// --------------------------------------------------------------------------------------------------
// <copyright file="ISmsService.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentPOS.Shared.DTOs.Sms;

namespace FluentPOS.Shared.Core.Interfaces.Services
{
    public interface ISmsService
    {
        Task SendAsync(SmsRequest request);
    }
}