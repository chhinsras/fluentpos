using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using System;

namespace FluentPOS.Modules.Catalog.Core.Features.Categories.Queries
{
    public class GetCategoryImageQuery : IRequest<Result<string>>
    {
        public Guid Id { get; }

        public GetCategoryImageQuery(Guid categoryId)
        {
            Id = categoryId;
        }
    }
}