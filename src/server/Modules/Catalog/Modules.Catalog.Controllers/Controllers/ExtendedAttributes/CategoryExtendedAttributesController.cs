using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Filters;
using FluentPOS.Shared.Infrastructure.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.Core.Features.Common.Filters;

namespace FluentPOS.Modules.Catalog.Controllers.ExtendedAttributes
{
    [ApiVersion("1")]
    [Route(BaseController.BasePath + "/" + nameof(Category) + "/attributes")]
    internal sealed class CategoryExtendedAttributesController : ExtendedAttributesController<Guid, Category>
    {
        [Authorize(Policy = Permissions.CategoriesExtendedAttributes.ViewAll)]
        public override Task<IActionResult> GetAllAsync(PaginatedExtendedAttributeFilter<Guid, Category> filter)
        {
            return base.GetAllAsync(filter);
        }

        [Authorize(Policy = Permissions.CategoriesExtendedAttributes.View)]
        public override Task<IActionResult> GetByIdAsync([FromQuery] GetByIdCacheableFilter<Guid, ExtendedAttribute<Guid, Category>> filter)
        {
            return base.GetByIdAsync(filter);
        }

        [Authorize(Policy = Permissions.CategoriesExtendedAttributes.Add)]
        public override Task<IActionResult> CreateAsync(AddExtendedAttributeCommand<Guid, Category> command)
        {
            return base.CreateAsync(command);
        }

        [Authorize(Policy = Permissions.CategoriesExtendedAttributes.Update)]
        public override Task<IActionResult> UpdateAsync(UpdateExtendedAttributeCommand<Guid, Category> command)
        {
            return base.UpdateAsync(command);
        }

        [Authorize(Policy = Permissions.CategoriesExtendedAttributes.Remove)]
        public override Task<IActionResult> RemoveAsync(Guid id)
        {
            return base.RemoveAsync(id);
        }
    }
}