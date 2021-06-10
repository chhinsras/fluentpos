using AutoMapper;
using FluentPOS.Modules.Catalog.Core.Abstractions;
using FluentPOS.Modules.Catalog.Core.Exceptions;
using FluentPOS.Shared.Abstractions.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Core.Features.Categories.Queries
{
    public class GetCategoryByIdQuery : IRequest<Result<GetCategoryByIdResponse>>
    {
        public Guid Id { get; set; }
    }

    internal class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Result<GetCategoryByIdResponse>>
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetCategoryByIdQueryHandler> _localizer;

        public GetCategoryByIdQueryHandler(ICatalogDbContext context, IMapper mapper, IStringLocalizer<GetCategoryByIdQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<GetCategoryByIdResponse>> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(c => c.Id == query.Id);
            if (category == null) throw new CatalogException(_localizer["Category Not Found!"]);
            var mappedCategory = _mapper.Map<GetCategoryByIdResponse>(category);
            return await Result<GetCategoryByIdResponse>.SuccessAsync(mappedCategory);
        }
    }
}