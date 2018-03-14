using System;

namespace Ci.Extension.NetCore
{
    public static class GuidExtensions
    {
        public static bool IsNullOrEmpty(this Guid value)
        {
            return value == Guid.Empty;
        }
    }
}
