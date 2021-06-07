using Microsoft.AspNetCore.Mvc;

namespace FluentPOS.Modules.Catalog.Controllers
{
    internal class ProductsController : BaseController
    {
        [HttpGet]
        public ActionResult<string> Get() => "Catalog module";
    }
}