using EventBus.Application.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace EventBus.Application.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
