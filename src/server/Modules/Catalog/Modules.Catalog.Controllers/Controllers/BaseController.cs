using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Modules.Catalog.Controllers
{
    [ApiController]
    [Route(BasePath + "/[controller]")]
    internal abstract class BaseController : ControllerBase
    {
        protected internal const string BasePath = "api/catalog";

        private IMediator _mediatorInstance;
        protected IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();

        private IMapper _mapperInstance;
        protected IMapper Mapper => _mapperInstance ??= HttpContext.RequestServices.GetService<IMapper>();
    }
}