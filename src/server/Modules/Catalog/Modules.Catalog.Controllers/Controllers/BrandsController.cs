using FluentPOS.Modules.Catalog.Core.Features.Brands.Commands;
using FluentPOS.Modules.Catalog.Core.Features.Brands.Queries;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.DTOs.Catalogs.Brands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Shared.Core.Features.Common.Filters;

namespace FluentPOS.Modules.Catalog.Controllers
{
    [ApiVersion("1")]
    internal sealed class BrandsController : BaseController
    {
        [HttpGet("{id}")]
        [Authorize(Policy = Permissions.Brands.View)]
        public async Task<IActionResult> GetByIdAsync([FromQuery] GetByIdCacheableFilter<Guid, Brand> filter)
        {
            var request = Mapper.Map<GetBrandByIdQuery>(filter);
            var brand = await Mediator.Send(request);
            return Ok(brand);
        }

        [HttpGet]
        [Authorize(Policy = Permissions.Brands.ViewAll)]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginatedBrandFilter filter)
        {
            var request = Mapper.Map<GetBrandsQuery>(filter);
            var brands = await Mediator.Send(request);
            return Ok(brands);
        }

        [HttpPost]
        [Authorize(Policy = Permissions.Brands.Register)]
        public async Task<IActionResult> RegisterAsync(RegisterBrandCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        [Authorize(Policy = Permissions.Brands.Update)]
        public async Task<IActionResult> UpdateAsync(UpdateBrandCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Permissions.Brands.Remove)]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            return Ok(await Mediator.Send(new RemoveBrandCommand(id)));
        }
    }
}