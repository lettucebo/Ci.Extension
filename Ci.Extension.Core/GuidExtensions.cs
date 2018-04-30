using System;

namespace Ci.Extension.Core
{
    public static class GuidExtensions
    {
        public static bool IsNullOrEmpty(this Guid value)
        {
            return value == Guid.Empty;
        }
    }
}
