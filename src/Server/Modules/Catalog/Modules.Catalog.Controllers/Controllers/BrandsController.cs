using Microsoft.AspNetCore.Mvc;

namespace FluentPOS.Modules.Catalog.Controllers
{
    internal class BrandsController : BaseController
    {
        [HttpGet]
        public ActionResult<string> Get() => "Brands module";
    }
}