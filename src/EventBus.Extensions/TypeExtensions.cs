using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Extensions
{
    public static class TypeExtensions
    {
        public static bool HasInterface(this Type type,string name)
        {
            if(name.IsNullOrEmpty()) return false;

            var interfaceType = type.GetInterface(name);
            return interfaceType != null;
        }
    }
}
