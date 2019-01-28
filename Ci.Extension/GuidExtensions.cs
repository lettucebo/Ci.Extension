using System;

namespace Ci.Extension
{
    public static class GuidExtensions
    {
        public static bool IsNullOrEmpty(this Guid value)
        {
            return value == Guid.Empty;
        }
    }
}
