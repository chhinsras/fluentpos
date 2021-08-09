// --------------------------------------------------------------------------------------------------
// <copyright file="CommonBaseController.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Shared.Infrastructure.Controllers
{
    [ApiController]
    [Route(BasePath + "/[controller]")]
    public abstract class CommonBaseController : ControllerBase
    {
        protected internal const string BasePath = "api/v{version:apiVersion}";

        private IMediator _mediatorInstance;

        protected IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();

        private IMapper _mapperInstance;

        protected IMapper Mapper => _mapperInstance ??= HttpContext.RequestServices.GetService<IMapper>();
    }
}