using Microsoft.AspNetCore.Mvc;

namespace EventBus.Application.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
