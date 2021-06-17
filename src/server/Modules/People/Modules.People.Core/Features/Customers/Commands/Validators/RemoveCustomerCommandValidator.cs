using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.People.Core.Features.Customers.Commands.Validators
{
    public class RemoveCustomerCommandValidator : AbstractValidator<RemoveCustomerCommand>
    {
        public RemoveCustomerCommandValidator(IStringLocalizer<RemoveCustomerCommandValidator> localizer)
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
