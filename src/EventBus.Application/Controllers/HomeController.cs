using EventBus.Application.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace EventBus.Application.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet("/[controller]")]
        [HttpGet("/[controller]/[action]")]
        public IActionResult Index()
        {
            return RedirectToAction("index", "event");
        }
    }
}
