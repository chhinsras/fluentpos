using System;
using System.Threading.Tasks;
using FluentPOS.Modules.Catalog.Core.Features.Products.Queries;
using FluentPOS.Shared.Core.Interfaces.Services.Catalog;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Products;
using MediatR;

namespace FluentPOS.Modules.Catalog.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IMediator _mediator;

        public ProductService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Result<GetProductByIdResponse>> GetDetails(Guid productId)
        {
            return await _mediator.Send(new GetProductByIdQuery(productId, false));
        }
    }
}