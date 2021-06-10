using FluentPOS.Modules.Catalog.Core.Features.Categories.Commands;
using FluentPOS.Modules.Catalog.Core.Features.Categories.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Controllers
{
    internal class CategoriesController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _mediator.Send(new GetAllCategoriesQuery());
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var category = await _mediator.Send(new GetCategoryByIdQuery() { Id = id });
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddEditCategoryCommand command)
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