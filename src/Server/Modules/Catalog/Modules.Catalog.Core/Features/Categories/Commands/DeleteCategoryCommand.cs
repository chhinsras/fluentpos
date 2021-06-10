using FluentPOS.Modules.Catalog.Core.Abstractions;
using FluentPOS.Modules.Catalog.Core.Exceptions;
using FluentPOS.Shared.Abstractions.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        private readonly ICatalogDbContext _context;
        private readonly IStringLocalizer<DeleteCategoryCommandHandler> _localizer;

        public DeleteCategoryCommandHandler(ICatalogDbContext context, IStringLocalizer<DeleteCategoryCommandHandler> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<Result<Guid>> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
        {
            var isCategoryUsed = await IsCategoryUsed(command.Id);
            if (!isCategoryUsed)
            {
                var category = await _context.Categories.FirstOrDefaultAsync(b => b.Id == command.Id);
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync(cancellationToken);
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