// --------------------------------------------------------------------------------------------------
// <copyright file="ValidatorInterceptor.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Linq;
using FluentPOS.Shared.Core.Exceptions;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Shared.Infrastructure.Interceptors
{
    public class ValidatorInterceptor : IValidatorInterceptor
    {
        private readonly IStringLocalizer<ValidatorInterceptor> _localizer;

        public ValidatorInterceptor(IStringLocalizer<ValidatorInterceptor> localizer)
        {
            _localizer = localizer;
        }

        public IValidationContext BeforeAspNetValidation(ActionContext actionContext, IValidationContext commonContext)
        {
            return commonContext;
        }

        public ValidationResult AfterAspNetValidation(ActionContext actionContext, IValidationContext validationContext, ValidationResult result)
        {
            var failures = result.Errors.Where(f => f != null).ToList();

            if (failures.Count != 0)
            {
                var errorMessages = failures.Select(a => a.ErrorMessage).Distinct().ToList();
                throw new CustomValidationException(_localizer, errorMessages);
            }

            return result;
        }
    }
}