using FluentPOS.Modules.Catalog.Core.Features.Categories.Commands;
using FluentPOS.Modules.Catalog.Core.Features.Categories.Queries;
using FluentPOS.Shared.DTOs.Catalogs.Categories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Controllers
{
    internal class CategoriesController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginatedCategoryFilter filter)
        {
            var categories = await _mediator.Send(new GetAllPagedCategoriesQuery(filter.PageNumber, filter.PageSize, filter.SearchString));
            return Ok(categories);
        }
       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, bool bypassCache)
        {
            var category = await _mediator.Send(new GetCategoryByIdQuery() { Id = id, BypassCache = bypassCache});
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddCategoryCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> Update(EditCategoryCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteCategoryCommand { Id = id }));
        }
    }
}