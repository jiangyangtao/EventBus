using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Extensions
{
    public static class ICollectionExtensions
    {
        public static bool IsNullOrEmpty<T>(this ICollection<T> values) => values == null || values.Count == 0;

        public static bool NotNullAndEmpty<T>(this ICollection<T> values) => values.IsNullOrEmpty() == false;
    }
}
