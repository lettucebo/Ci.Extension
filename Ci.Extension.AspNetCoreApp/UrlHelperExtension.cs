using System;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Ci.Extension.AspNetCoreApp
{
    public static class UrlHelperExtension
    {
        public static string AdaptContent(this UrlHelper url, string contentPath)
        {
            if (contentPath.StartsWith("http"))
                return contentPath;

            if (contentPath.StartsWith("~"))
                return url.Content(contentPath);

            throw new NotSupportedException($"{contentPath} string is not support.");
        }
    }
}
