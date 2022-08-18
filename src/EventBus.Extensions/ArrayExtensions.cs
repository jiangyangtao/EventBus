using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Extensions
{
    public static class ArrayExtensions
    {
        public static bool IsNullOrEmpty<T>(this T[] array) => array == null || array.Length == 0;

        public static bool NotNullAndEmpty<T>(this T[] array) => array.IsNullOrEmpty() == false;
    }
}
