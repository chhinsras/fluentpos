using AutoMapper;
using FluentPOS.Modules.Catalog.Core.Abstractions;
using FluentPOS.Modules.Catalog.Core.Exceptions;
using FluentPOS.Shared.Abstractions.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Core.Features.Products.Queries
{
    public class GetProductByIdQuery : IRequest<Result<GetProductByIdResponse>>
    {
        public Guid Id { get; set; }
    }

    internal class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<GetProductByIdResponse>>
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetProductByIdQueryHandler> _localizer;

        public GetProductByIdQueryHandler(ICatalogDbContext context, IMapper mapper, IStringLocalizer<GetProductByIdQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<GetProductByIdResponse>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await _context.Products.Where(p => p.Id == query.Id).FirstOrDefaultAsync(cancellationToken);
            if (product == null) throw new CatalogException(_localizer["Product Not Found!"]);
            var mappedProduct = _mapper.Map<GetProductByIdResponse>(product);
            return await Result<GetProductByIdResponse>.SuccessAsync(mappedProduct);
        }
    }
}