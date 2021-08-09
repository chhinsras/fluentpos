// --------------------------------------------------------------------------------------------------
// <copyright file="ValidatorExtensions.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentPOS.Shared.Core.Interfaces.Serialization;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Shared.Core.Extensions
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string?> MustBeJson<T>(this IRuleBuilderInitial<T, string?> ruleBuilder, IJsonSerializer jsonSerializer)
            => ruleBuilder
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(value =>
                {
                    if (value == null)
                    {
                        return false;
                    }

                    bool isJson = true;
                    value = value.Trim();
                    try
                    {
                        jsonSerializer.Deserialize<object>(value);
                    }
                    catch
                    {
                        isJson = false;
                    }

                    return (isJson && value.StartsWith("{") && value.EndsWith("}")) || (value.StartsWith("[") && value.EndsWith("]"));
                })
                .WithMessage("'{PropertyName}' must be a valid JSON string.");

        public static IRuleBuilderOptions<T, string?> MustContainCorrectOrderingsFor<T>(
            this IRuleBuilderInitial<T, string?> ruleBuilder,
            Type orderedType,
            IStringLocalizer localizer)
                where T : class
            => ruleBuilder
                .Cascade(CascadeMode.Stop)
                .Must((_, value, context) =>
                {
                    string[]? orderings = new OrderByConverter().Convert(value);
                    if (orderings == null)
                    {
                        return true;
                    }

                    bool result = true;
                    var orderedProperties = new List<string>();
                    var propertyNames = orderedType
                        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Select(p => p.Name.ToLowerInvariant())
                        .ToList();
                    foreach (string? ordering in orderings)
                    {
                        var orderingParts = ordering
                            .Trim()
                            .Split(" ")
                            .Where(x => !string.IsNullOrWhiteSpace(x))
                            .Select(x => x.Trim().ToLowerInvariant())
                            .ToList();
                        if (orderingParts.Count != 2)
                        {
                            context.AddFailure(string.Format(localizer["Ordering '{0}' does not contains 2 parts."]!, ordering));
                            result = false;
                            continue;
                        }

                        string propertyName = orderingParts[0];
                        switch (orderingParts.Last())
                        {
                            case "asc":
                            case "desc":
                            case "ascending":
                            case "descending":
                                break;
                            default:
                                context.AddFailure(string.Format(localizer["Ordering '{0}' does not contains correct sort direction."]!, ordering));
                                result = false;
                                continue;
                        }

                        if (!propertyNames.Contains(propertyName))
                        {
                            context.AddFailure(string.Format(localizer["Ordering '{0}' contains wrong property name."]!, ordering));
                            result = false;
                        }

                        if (orderedProperties.Contains(propertyName))
                        {
                            context.AddFailure(string.Format(localizer["Ordering '{0}' contains already used property name."]!, ordering));
                            result = false;
                            continue;
                        }

                        orderedProperties.Add(propertyName);
                    }

                    return result;
                })
                .WithMessage(_ => localizer["The {PropertyName} property must contain correct comma separated orderings: '<property1> <direction>,<property2> <direction>'."]);
    }
}