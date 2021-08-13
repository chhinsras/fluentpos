// --------------------------------------------------------------------------------------------------
// <copyright file="MvcBuilderExtensions.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Reflection;
using FluentPOS.Modules.People.Core.Abstractions;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Modules.People.Infrastructure.Extensions
{
    internal static class MvcBuilderExtensions
    {
        internal static IMvcBuilder AddPeopleValidation(this IMvcBuilder builder)
        {
            return builder.AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.GetAssembly(typeof(IPeopleDbContext))));
        }
    }
}