using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ci.Extensions
{
    public static class GuidExtensions
    {
        public static bool IsNullOrEmpty(this Guid value)
        {
            return value == Guid.Empty;
        }
    }
}
