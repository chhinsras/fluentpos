using System;
using System.Threading.Tasks;
using FluentPOS.Modules.Identity.Core.Entities;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands;
using FluentPOS.Shared.DTOs.ExtendedAttributes;
using FluentPOS.Shared.Infrastructure.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Modules.Identity.Controllers
{
    [Route(BaseController.BasePath + "/user/attributes")]
    public class UserExtendedAttributesController : ExtendedAttributesController<string, FluentUser>
    {
        private IMediator _mediatorInstance;
        protected override IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();

        [Authorize(Policy = Permissions.UsersExtendedAttributes.ViewAll)]
        public override Task<IActionResult> GetAll(PaginatedExtendedAttributeFilter<string> filter)
        {
            return base.GetAll(filter);
        }

        [Authorize(Policy = Permissions.UsersExtendedAttributes.View)]
        public override Task<IActionResult> GetById(Guid id, bool bypassCache)
        {
            return base.GetById(id, bypassCache);
        }

        [Authorize(Policy = Permissions.UsersExtendedAttributes.Add)]
        public override Task<IActionResult> Create(AddExtendedAttributeCommand<string, FluentUser> command)
        {
            return base.Create(command);
        }

        [Authorize(Policy = Permissions.UsersExtendedAttributes.Update)]
        public override Task<IActionResult> Update(UpdateExtendedAttributeCommand<string, FluentUser> command)
        {
            return base.Update(command);
        }

        [Authorize(Policy = Permissions.UsersExtendedAttributes.Remove)]
        public override Task<IActionResult> Remove(Guid id)
        {
            return base.Remove(id);
        }
    }
}