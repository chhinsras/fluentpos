using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Modules.People.Controllers
{
    [ApiController]
    [Route(BasePath + "/[controller]")]
    internal abstract class BaseController : ControllerBase
    {
        protected internal const string BasePath = "api/people";

        private IMediator _mediatorInstance;
        protected IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}