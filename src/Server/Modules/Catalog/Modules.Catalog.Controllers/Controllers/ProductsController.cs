using FluentPOS.Modules.Catalog.Core.Features.Products.Commands;
using FluentPOS.Modules.Catalog.Core.Features.Products.Queries;
using FluentPOS.Shared.DTOs.Catalogs.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Controllers
{
    internal class ProductsController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginatedProductFilter filter)
        {
            var brands = await _mediator.Send(new GetAllPagedProductsQuery(filter.PageNumber, filter.PageSize, filter.SearchString));
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, bool bypassCache)
        {
            var brand = await _mediator.Send(new GetProductByIdQuery() { Id = id, BypassCache = bypassCache });
            return Ok(brand);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddProductCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> Update(EditProductCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteProductCommand { Id = id }));
        }
    }
}