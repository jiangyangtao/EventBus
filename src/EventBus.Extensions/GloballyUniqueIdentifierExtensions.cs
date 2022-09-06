using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Extensions
{
    public static class GloballyUniqueIdentifierExtensions
    {
        public static bool IsEmpty(this Guid id) => id == Guid.Empty;

        public static bool NotEmpty(this Guid id) => id.IsEmpty() == false;
    }
}
