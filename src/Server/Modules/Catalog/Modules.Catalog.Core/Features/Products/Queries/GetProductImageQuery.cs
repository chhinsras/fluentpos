using FluentPOS.Modules.Catalog.Core.Abstractions;
using FluentPOS.Shared.Abstractions.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Core.Features.Products.Queries
{
    public class GetProductImageQuery : IRequest<Result<string>>
    {
        public Guid Id { get; set; }

        public GetProductImageQuery(Guid productId)
        {
            Id = productId;
        }
    }

    internal class GetBrandImageQueryHandler : IRequestHandler<GetProductImageQuery, Result<string>>
    {
        private readonly ICatalogDbContext _context;

        public GetBrandImageQueryHandler(ICatalogDbContext context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(GetProductImageQuery query, CancellationToken cancellationToken)
        {
            var data = await _context.Brands.Where(p => p.Id == query.Id).Select(x => x.ImageUrl).FirstOrDefaultAsync(cancellationToken);
            return await Result<string>.SuccessAsync(data: data);
        }
    }
}