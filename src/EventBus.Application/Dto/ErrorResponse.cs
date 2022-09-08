using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EventBus.Application.Dto
{
    public class ErrorResponse : Exception
    {
        public ErrorResponse(int httpStatusCode, string errorCode, string errorMessage)
        {
            HttpStatusCode = httpStatusCode;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// HTTP 状态码
        /// </summary>
        public int HttpStatusCode { get; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public string ErrorCode { get; }


        public string ErrorMessage { get; }

        public Exception GetException() => this;

        public string GetResponseResult()
        {
            var json = new JObject
            (
                new JProperty("Code", ErrorCode),
                new JProperty("Message", ErrorMessage)
            );

            return json.ToString(Formatting.None);
        }
    }
}
