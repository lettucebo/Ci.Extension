using System;
using Microsoft.AspNetCore.Http;

namespace Ci.Extension.AspNetCore
{
    public static class HttpContextExtension
    {
        public static string AdaptContent(this HttpContext context, string contentPath)
        {
            if (string.IsNullOrWhiteSpace(contentPath))
                throw new ArgumentNullException(nameof(contentPath));

            if (contentPath.StartsWith("http"))
                return contentPath;

            if (contentPath.StartsWith("~"))
            {
                var webRoot =
                    $"{context.Request.Scheme}://{context.Request.Host}{context.Request.PathBase}";
                var path = webRoot + contentPath.Substring(1);
                return path;

            }

            return contentPath;
        }
    }
}
