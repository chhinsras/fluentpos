using Microsoft.AspNetCore.Mvc;

namespace FluentPOS.Modules.Identity.Controllers
{
    [ApiController]
    [Route(BasePath + "/[controller]")]
    internal abstract class BaseController : ControllerBase
    {
        protected internal const string BasePath = "api/identity";
    }
}