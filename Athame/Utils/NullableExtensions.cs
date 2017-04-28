using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athame.Utils
{
    public static class NullableExtensions
    {
        public static bool ValueOrFalse(this bool? obj)
        {
            return obj != null && obj.Value;
        }
    }
}
