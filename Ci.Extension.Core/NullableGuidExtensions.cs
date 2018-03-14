using System;

namespace Ci.Extension.NetCore
{
    public static class NullableGuidExtensions
    {
        public static bool IsNullOrEmpty(this Guid? value)
        {
            return (!value.HasValue || value == Guid.Empty);
        }
    }
}
