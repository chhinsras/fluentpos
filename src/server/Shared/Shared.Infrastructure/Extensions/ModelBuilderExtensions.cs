// --------------------------------------------------------------------------------------------------
// <copyright file="ModelBuilderExtensions.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Linq;
using FluentPOS.Shared.Core.Settings;
using Microsoft.EntityFrameworkCore;

namespace FluentPOS.Shared.Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyApplicationConfiguration(this ModelBuilder builder, PersistenceSettings persistenceOptions)
        {
            // build model for MSSQL and Postgres

            if (persistenceOptions.UseMsSql)
            {
                foreach (var property in builder.Model.GetEntityTypes()
                    .SelectMany(t => t.GetProperties())
                    .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
                {
                    property.SetColumnType("decimal(23,2)");
                }
            }
        }

        public static void ApplyModuleConfiguration(this ModelBuilder builder, PersistenceSettings persistenceOptions)
        {
            // build model for MSSQL and Postgres
        }
    }
}