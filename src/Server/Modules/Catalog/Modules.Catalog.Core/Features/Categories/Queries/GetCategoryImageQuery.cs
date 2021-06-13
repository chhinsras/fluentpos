using FluentPOS.Modules.Catalog.Core.Abstractions;
using FluentPOS.Shared.Application.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Core.Features.Categories.Queries
{
    public class GetCategoryImageQuery : IRequest<Result<string>>
    {
        public Guid Id { get; set; }

        public GetCategoryImageQuery(Guid categoryId)
        {
            Id = categoryId;
        }
    }

    internal class GetCategoryImageQueryHandler : IRequestHandler<GetCategoryImageQuery, Result<string>>
    {
        private readonly ICatalogDbContext _context;

        public GetCategoryImageQueryHandler(ICatalogDbContext context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(GetCategoryImageQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.Categories.Where(c => c.Id == request.Id).Select(a => a.ImageUrl).FirstOrDefaultAsync(cancellationToken);
            return await Result<string>.SuccessAsync(data: data);
        }
    }
}