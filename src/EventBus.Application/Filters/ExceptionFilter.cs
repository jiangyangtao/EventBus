using EventBus.Application.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EventBus.Application.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;
            var errorResponse = GetErrorResponse(context.Exception);
            context.Result = new ActionResult(errorResponse);
        }

        private static ErrorResponse GetErrorResponse(Exception exception)
        {
            if (exception is ErrorResponse error) return error;

            return new ErrorResponse(500, "01001", exception.Message);
        }
    }

    internal class ActionResult : IActionResult
    {
        private readonly ErrorResponse Error;

        public ActionResult(ErrorResponse error)
        {
            Error = error;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.StatusCode = Error.HttpStatusCode;
            context.HttpContext.Response.ContentType = "application/json";

            var result = Error.GetResponseResult();
            await context.HttpContext.Response.WriteAsync(result);
        }
    }
}
