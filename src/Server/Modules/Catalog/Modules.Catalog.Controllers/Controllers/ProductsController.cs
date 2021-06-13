using FluentPOS.Modules.Catalog.Core.Features.Products.Commands;
using FluentPOS.Modules.Catalog.Core.Features.Products.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Controllers
{
    internal class ProductsController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string searchString)
        {
            var brands = await _mediator.Send(new GetAllPagedProductsQuery(pageNumber, pageSize, searchString));
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var brand = await _mediator.Send(new GetProductByIdQuery() { Id = id });
            return Ok(brand);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddEditProductCommand command)
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