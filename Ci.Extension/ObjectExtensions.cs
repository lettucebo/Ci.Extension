using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ci.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// null轉為空字串
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>string</returns>
        public static string FieldToString(this object field)
        {
            return field == null ? string.Empty : field.ToString().Trim();
        }
    }
}
