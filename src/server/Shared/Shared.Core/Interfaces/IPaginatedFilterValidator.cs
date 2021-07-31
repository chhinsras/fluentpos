using FluentPOS.Shared.Core.Contracts;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.DTOs.Filters;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Shared.Core.Interfaces
{
    internal interface IPaginatedFilterValidator<TEntityId, TEntity, TFilter>
        where TFilter : PaginatedFilter
        where TEntity : class, IEntity<TEntityId>
    {
        static void UseRules(AbstractValidator<TFilter> validator, IStringLocalizer localizer)
        {
            validator.RuleFor(request => request.PageNumber)
                .GreaterThan(0).WithMessage(localizer["The {PropertyName} property must be greater than 0."]);
            validator.RuleFor(request => request.PageSize)
                .GreaterThan(0).WithMessage(localizer["The {PropertyName} property must be greater than 0."]);
            validator.RuleFor(request => request.OrderBy)
                .MustContainCorrectOrderingsFor(typeof(TEntity), localizer);
        }
    }
}