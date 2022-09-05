using Microsoft.AspNetCore.Mvc;

namespace EventBus.Application.Controllers.Base
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseApiController : Controller
    {
    }
}
