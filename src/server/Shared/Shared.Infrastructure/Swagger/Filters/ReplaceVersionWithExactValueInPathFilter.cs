// --------------------------------------------------------------------------------------------------
// <copyright file="ReplaceVersionWithExactValueInPathFilter.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FluentPOS.Shared.Infrastructure.Swagger.Filters
{
    public class ReplaceVersionWithExactValueInPathFilter : IDocumentFilter
    {
        /// <inheritdoc/>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = new OpenApiPaths();

            foreach (var (key, value) in swaggerDoc.Paths)
                paths.Add(key.Replace("v{version}", swaggerDoc.Info.Version, StringComparison.InvariantCultureIgnoreCase), value);

            swaggerDoc.Paths = paths;
        }
    }
}