using FluentPOS.Shared.Infrastructure.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace FluentPOS.Modules.Catalog.Controllers
{
    [ApiController]
    [Route(BasePath + "/[controller]")]
    internal abstract class BaseController : CommonBaseController
    {
        protected internal new const string BasePath = CommonBaseController.BasePath + "/catalog";
    }
}