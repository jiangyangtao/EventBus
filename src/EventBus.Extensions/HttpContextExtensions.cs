using Microsoft.AspNetCore.Http;
using System.Net;

namespace EventBus.Extensions
{
    public static class HttpContextExtensions
    {
        private static string[] IPAddressKeys = new string[] { "X-Real-IP", "X-Forwarded-For" };

        public static IPAddress GetClientIPAddress(this HttpContext context)
        {
            var ipaddress = context.Connection.RemoteIpAddress;
            if (ipaddress == null) return null;

            if (ipaddress.IsLocal() == false) return ipaddress;

            var headers = context.Request.Headers;
            foreach (var key in IPAddressKeys)
            {
                if (headers.ContainsKey(key))
                {
                    ipaddress = headers.GetIPAddress(key);
                    if (ipaddress.IsLocal() == false) return ipaddress;
                }
            }

            return null;
        }

        private static IPAddress GetIPAddress(this IHeaderDictionary headers, string headerKey)
        {
            return IPAddress.Parse(headers[headerKey].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries)[0]);
        }
    }
}
