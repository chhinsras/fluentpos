using AutoMapper;
using FluentPOS.Modules.Catalog.Infrastructure.Persistence;
using FluentPOS.Shared.Abstractions.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalogs.Infrastructure.Features.Brands.Queries.GetById
{
    public class GetBrandByIdQuery : IRequest<Result<GetBrandByIdResponse>>
    {
        public Guid Id { get; set; }
    }

    internal class GetProductByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, Result<GetBrandByIdResponse>>
    {
        private readonly CatalogDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetProductByIdQueryHandler> _localizer;

        public GetProductByIdQueryHandler(CatalogDbContext context, IMapper mapper, IStringLocalizer<GetProductByIdQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<GetBrandByIdResponse>> Handle(GetBrandByIdQuery query, CancellationToken cancellationToken)
        {
            var brand = await _context.Brands.SingleOrDefaultAsync(b => b.Id == query.Id);
            if (brand == null) return await Result<GetBrandByIdResponse>.FailAsync(_localizer["Brand Not Found!"]);
            var mappedBrand = _mapper.Map<GetBrandByIdResponse>(brand);
            return await Result<GetBrandByIdResponse>.SuccessAsync(mappedBrand);
        }
    }
}
