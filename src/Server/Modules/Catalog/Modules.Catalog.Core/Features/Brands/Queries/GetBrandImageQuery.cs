using FluentPOS.Modules.Catalog.Core.Abstractions;
using FluentPOS.Shared.Abstractions.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Core.Features.Brands.Queries
{
    public class GetBrandImageQuery : IRequest<Result<string>>
    {
        public Guid Id { get; set; }

        public GetBrandImageQuery(Guid brandId)
        {
            Id = brandId;
        }
    }

    internal class GetBrandImageQueryHandler : IRequestHandler<GetBrandImageQuery, Result<string>>
    {
        private readonly ICatalogDbContext _context;

        public GetBrandImageQueryHandler(ICatalogDbContext context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(GetBrandImageQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.Brands.Where(b => b.Id == request.Id).Select(a => a.ImageUrl).FirstOrDefaultAsync(cancellationToken);
            return await Result<string>.SuccessAsync(data: data);
        }
    }
}