// --------------------------------------------------------------------------------------------------
// <copyright file="OrderByConverter.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using AutoMapper;

namespace FluentPOS.Shared.Core.Mappings.Converters
{
    public class OrderByConverter :
        IValueConverter<string, string[]>,
        IValueConverter<string[], string>
    {
        /// <inheritdoc/>
        public string[] Convert(string orderBy, ResolutionContext context = null)
        {
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                return orderBy
                    .Split(',')
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(x => x.Trim()).ToArray();
            }

            return Array.Empty<string>();
        }

        /// <inheritdoc/>
        public string Convert(string[] orderBy, ResolutionContext context = null) => orderBy?.Any() == true ? string.Join(",", orderBy) : null;
    }
}