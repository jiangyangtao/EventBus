using Microsoft.AspNetCore.Mvc;

namespace EventBus.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Ok("Hello, EventBus");
        }
    }
}
