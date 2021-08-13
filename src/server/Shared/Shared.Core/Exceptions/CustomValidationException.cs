// --------------------------------------------------------------------------------------------------
// <copyright file="CustomValidationException.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Net;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Shared.Core.Exceptions
{
    public class CustomValidationException : CustomException
    {
        public CustomValidationException(IStringLocalizer localizer, List<string> errors)
            : base(localizer["One or more validation failures have occurred."], errors, HttpStatusCode.BadRequest)
        {
        }
    }
}