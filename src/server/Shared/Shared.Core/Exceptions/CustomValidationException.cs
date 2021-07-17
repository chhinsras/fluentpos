using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Net;

namespace FluentPOS.Shared.Core.Exceptions
{
    public class CustomValidationException : CustomException
    {
        public CustomValidationException(IStringLocalizer localizer, List<string> errors) : base(localizer["One or more validation failures have occurred."], errors, HttpStatusCode.BadRequest)
        {
        }
    }
}