using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Extensions
{
    public static class GenericExtensions
    {
        public static string GetGenericClassName<T>(this T value) => typeof(T).Name;
    }
}
