using FluentPOS.Modules.Catalog.Infrastructure.Persistence;
using FluentPOS.Shared.Abstractions.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalogs.Infrastructure.Features.Brands.Queries.GetImage
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
        private readonly CatalogDbContext _context;

        public GetBrandImageQueryHandler(CatalogDbContext context)
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
