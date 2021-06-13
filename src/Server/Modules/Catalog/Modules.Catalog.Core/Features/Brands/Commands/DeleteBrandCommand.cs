using FluentPOS.Modules.Catalog.Core.Abstractions;
using FluentPOS.Modules.Catalog.Core.Constants;
using FluentPOS.Modules.Catalog.Core.Exceptions;
using FluentPOS.Shared.Application.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Core.Features.Brands.Commands
{
    public class DeleteBrandCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }

    internal class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, Result<Guid>>
    {
        private readonly IDistributedCache _cache;
        private readonly ICatalogDbContext _context;
        private readonly IStringLocalizer<DeleteBrandCommandHandler> _localizer;

        public DeleteBrandCommandHandler(ICatalogDbContext context, IStringLocalizer<DeleteBrandCommandHandler> localizer, IDistributedCache cache)
        {
            _context = context;
            _localizer = localizer;
            _cache = cache;
        }

        public async Task<Result<Guid>> Handle(DeleteBrandCommand command, CancellationToken cancellationToken)
        {
            var isBrandUsed = await IsBrandUsed(command.Id);
            if (!isBrandUsed)
            {
                var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == command.Id);
                _context.Brands.Remove(brand);
                await _context.SaveChangesAsync(cancellationToken);
                await _cache.RemoveAsync(CatalogCacheKeys.GetBrandByIdCacheKey(command.Id));
                return await Result<Guid>.SuccessAsync(brand.Id, _localizer["Brand Deleted"]);
            }
            else
            {
                throw new CatalogException(_localizer["Deletion Not Allowed"]);
            }
        }

        public async Task<bool> IsBrandUsed(Guid brandId)
        {
            return await _context.Products.AnyAsync(b => b.BrandId == brandId);
        }
    }
}