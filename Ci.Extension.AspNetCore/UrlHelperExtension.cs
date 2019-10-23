using System;
using Microsoft.AspNetCore.Mvc;

namespace Ci.Extension.AspNetCore
{
    public static class UrlHelperExtension
    {
        public static string AdaptContent(this IUrlHelper url, string contentPath)
        {
            if (contentPath.StartsWith("http"))
                return contentPath;

            if (contentPath.StartsWith("~"))
                return url.Content(contentPath);

            throw new NotSupportedException($"{contentPath} string is not support.");
        }
    }
}
