using EventBus.Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EventBus.Application.Controllers.Base
{
    [Route("v1/api/[controller]/[action]")]
    [ApiController]
    public class BaseApiController : Controller
    {
        public BaseApiController()
        {
        }

        /// <summary>
        /// HTTP 400 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        public void ResponseBadRequest(string message = "")
        {
            throw new ErrorResponse(400, "-1", message).GetException();
        }

        /// <summary>
        /// HTTP 404
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        public void ResponseNotFound(string message = "")
        {
            throw new ErrorResponse(404, "-1", message).GetException();
        }

        /// <summary>
        /// HTTP 500
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        public void ResponseServerError(string message = "")
        {
            throw new ErrorResponse(500, "-1", message).GetException();
        }


        /// <summary>
        /// HTTP 409
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        public void ResponseConflict(string message = "")
        {
            throw new ErrorResponse(409, "-1", message).GetException();
        }
    }
}
