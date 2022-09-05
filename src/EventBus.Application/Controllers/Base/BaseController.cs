using Microsoft.AspNetCore.Mvc;

namespace EventBus.Application.Controllers.Base
{
    [Route("[controller]/[action]")]
    [ApiController]
    public abstract class BaseController : Controller
    {
    }
}
