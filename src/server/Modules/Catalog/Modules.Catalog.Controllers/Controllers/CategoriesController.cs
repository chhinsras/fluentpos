using FluentPOS.Modules.Catalog.Core.Features.Categories.Commands;
using FluentPOS.Modules.Catalog.Core.Features.Categories.Queries;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.DTOs.Catalogs.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Shared.Core.Features.Common.Filters;

namespace FluentPOS.Modules.Catalog.Controllers
{
    [ApiVersion("1")]
    internal sealed class CategoriesController : BaseController
    {
        [HttpGet]
        [Authorize(Policy = Permissions.Categories.ViewAll)]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginatedCategoryFilter filter)
        {
            var request = Mapper.Map<GetCategoriesQuery>(filter);
            var categories = await Mediator.Send(request);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = Permissions.Categories.View)]
        public async Task<IActionResult> GetByIdAsync([FromQuery] GetByIdCacheableFilter<Guid, Category> filter)
        {
            var request = Mapper.Map<GetCategoryByIdQuery>(filter);
            var category = await Mediator.Send(request);
            return Ok(category);
        }

        [HttpPost]
        [Authorize(Policy = Permissions.Categories.Register)]
        public async Task<IActionResult> RegisterAsync(RegisterCategoryCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        [Authorize(Policy = Permissions.Categories.Update)]
        public async Task<IActionResult> UpdateAsync(UpdateCategoryCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Permissions.Categories.Remove)]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            return Ok(await Mediator.Send(new RemoveCategoryCommand(id)));
        }
    }
}