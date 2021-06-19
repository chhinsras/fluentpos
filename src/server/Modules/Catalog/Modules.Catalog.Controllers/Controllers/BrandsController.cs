using FluentPOS.Modules.Catalog.Core.Features.Brands.Commands;
using FluentPOS.Modules.Catalog.Core.Features.Brands.Queries;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.DTOs.Catalogs.Brands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Controllers
{
    internal class BrandsController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginatedBrandFilter filter)
        {
            var brands = await Mediator.Send(new GetAllPagedBrandsQuery(filter.PageNumber, filter.PageSize, filter.SearchString));
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, bool bypassCache)
        {
            var brand = await Mediator.Send(new GetBrandByIdQuery(id, bypassCache));
            return Ok(brand);
        }
        [Authorize(Policy = Permissions.Brands.Create)]
        [HttpPost]
        public async Task<IActionResult> Create(RegisterBrandCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateBrandCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new RemoveBrandCommand(id)));
        }
    }
}