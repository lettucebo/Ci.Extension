using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ci.Extensions
{
    using System.Drawing;
    using System.Drawing.Imaging;

    public static class ImageExtensions
    {
        /// <summary>
        /// Using ImageFormatConverter().ConvertToString()
        /// </summary>
        /// <param name="format">The format.</param>
        /// <returns>System.String.</returns>
        public static string ToFileExtension(this ImageFormat format)
        {
            if (format == null)
                throw new NullReferenceException();

            return "." + new ImageFormatConverter().ConvertToString(format).ToLower();
        }
    }
}
