using Microsoft.AspNetCore.Mvc;

namespace EventBus.Application.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
