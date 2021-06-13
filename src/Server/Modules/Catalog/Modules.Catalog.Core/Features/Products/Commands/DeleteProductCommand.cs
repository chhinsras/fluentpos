using FluentPOS.Modules.Catalog.Core.Abstractions;
using FluentPOS.Modules.Catalog.Core.Constants;
using FluentPOS.Modules.Catalog.Core.Exceptions;
using FluentPOS.Shared.Application.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Core.Features.Products.Commands
{
    public class DeleteProductCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }

    internal class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<Guid>>
    {
        private readonly IDistributedCache _cache;
        private readonly ICatalogDbContext _context;
        private readonly IStringLocalizer<DeleteProductCommandHandler> _localizer;

        public DeleteProductCommandHandler(ICatalogDbContext context, IStringLocalizer<DeleteProductCommandHandler> localizer, IDistributedCache cache)
        {
            _context = context;
            _localizer = localizer;
            _cache = cache;
        }

        public async Task<Result<Guid>> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _context.Products.Where(p => p.Id == command.Id).FirstOrDefaultAsync(cancellationToken);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync(cancellationToken);
                await _cache.RemoveAsync(CatalogCacheKeys.GetProductByIdCacheKey(command.Id));
                return await Result<Guid>.SuccessAsync(product.Id, _localizer["Product Deleted"]);
            }
            else
            {
                throw new CatalogException(_localizer["Product Not Found!"]);
            }
        }
    }
}
