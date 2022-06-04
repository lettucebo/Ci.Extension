using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ci.Extensions.Web
{
    using System.Web;

    /// <summary>
    /// Cookie Extensions.
    /// </summary>
    public static class CookieExtensions
    {
        /// <summary>
        /// 透過過期的方式來清除所有 Cookies
        /// </summary>
        /// <param name="cookies">The HttpCookieCollection.</param>
        /// <returns>HttpCookieCollection.</returns>
        public static HttpCookieCollection RemoveAll(this HttpCookieCollection cookies)
        {
            HttpContext.Current.Request.Cookies.ExpireAll(-1);
            return cookies;
        }

        public static HttpCookieCollection ExpireAll(this HttpCookieCollection cookies, int days)
        {
            var aCookies = HttpContext.Current.Request.Cookies;
            foreach (HttpCookie cookie in aCookies)
            {
                cookie.Expires = DateTime.Now.AddDays(days);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }

            return cookies;
        }
    }
}
