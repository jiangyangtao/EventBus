using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Extensions
{
    public static class IPAddressExtensions
    {
        public static bool IsLocal(this IPAddress address)
        {
            return address.ToString() == "127.0.0.1";
        }
    }
}
