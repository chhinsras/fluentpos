using FluentPOS.Shared.Core.Contracts;
using FluentPOS.Shared.Core.Interfaces;
using FluentPOS.Shared.DTOs.Filters;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Shared.Core.Features.Common.Queries.Validators
{
    public abstract class PaginatedFilterValidator<TEntityId, TEntity, TFilter> :
        AbstractValidator<TFilter>,
        IPaginatedFilterValidator<TEntityId, TEntity, TFilter>
            where TEntity : class, IEntity<TEntityId>
            where TFilter : PaginatedFilter
    {
        protected PaginatedFilterValidator(IStringLocalizer localizer)
        {
            IPaginatedFilterValidator<TEntityId, TEntity, TFilter>.UseRules(this, localizer);
        }
    }
}