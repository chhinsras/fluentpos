// --------------------------------------------------------------------------------------------------
// <copyright file="IEventLogger.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Shared.Core.EventLogging
{
    public interface IEventLogger
    {
        Task SaveAsync<T>(T @event, (string oldValues, string newValues) changes)
            where T : Event;
    }
}