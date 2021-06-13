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

namespace FluentPOS.Modules.Catalog.Core.Features.Categories.Commands
{
    public class DeleteCategoryCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }

    internal class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<Guid>>
    {
        private readonly IDistributedCache _cache;
        private readonly ICatalogDbContext _context;
        private readonly IStringLocalizer<DeleteCategoryCommandHandler> _localizer;

        public DeleteCategoryCommandHandler(ICatalogDbContext context, IStringLocalizer<DeleteCategoryCommandHandler> localizer, IDistributedCache cache)
        {
            _context = context;
            _localizer = localizer;
            _cache = cache;
        }

        public async Task<Result<Guid>> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
        {
            var isCategoryUsed = await IsCategoryUsed(command.Id);
            if (!isCategoryUsed)
            {
                var category = await _context.Categories.FirstOrDefaultAsync(b => b.Id == command.Id);
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync(cancellationToken);
                await _cache.RemoveAsync(CatalogCacheKeys.GetCategoryByIdCacheKey(command.Id));
                return await Result<Guid>.SuccessAsync(category.Id, _localizer["Category Deleted"]);
            }
            else
            {
                throw new CatalogException(_localizer["Deletion Not Allowed"]);
            }
        }

        public async Task<bool> IsCategoryUsed(Guid categoryId)
        {
            return await _context.Products.AnyAsync(c => c.CategoryId == categoryId);
        }
    }
}