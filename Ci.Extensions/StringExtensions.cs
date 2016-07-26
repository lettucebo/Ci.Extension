using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ci.Extensions
{
    /// <summary>
    /// String Extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 字串null也去空白
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToTrim(this string value)
        {
            return value == null ? string.Empty : value.Trim();
        }
    }
}
