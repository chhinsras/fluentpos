using FluentPOS.Modules.Catalog.Core.Features.Brands.Commands;
using FluentPOS.Modules.Catalog.Core.Features.Brands.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Controllers
{
    internal class BrandsController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string searchString)
        {
            var brands = await _mediator.Send(new GetAllPagedBrandsQuery(pageNumber, pageSize, searchString));
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, bool bypassCache)
        {
            var brand = await _mediator.Send(new GetBrandByIdQuery() { Id = id, BypassCache  = bypassCache });
            return Ok(brand);
        }
     
        [HttpPost]
        public async Task<IActionResult> Post(AddEditBrandCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
     
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteBrandCommand { Id = id }));
        }
    }
}