using System;
using Microsoft.AspNetCore.Mvc;

namespace Ci.Extension.AspNetCore
{
    public static class UrlHelperExtension
    {
        public static string AdaptContent(this IUrlHelper url, string contentPath)
        {
            if (string.IsNullOrWhiteSpace(contentPath))
                throw new ArgumentNullException(nameof(contentPath));

            if (contentPath.StartsWith("http"))
                return contentPath;

            if (contentPath.StartsWith("~"))
                return url.Content(contentPath);

            return contentPath;
        }

        /// <summary>
        /// Generates a fully qualified URL to the specified content by using the specified content path.
        /// </summary>
        /// <param name="url">The URL helper.</param>
        /// <param name="contentPath">The content path.</param>
        /// <returns>The absolute URL.</returns>
        public static string AbsoluteContent(this IUrlHelper url, string contentPath)
        {
            var request = url.ActionContext.HttpContext.Request;
            return new Uri(new Uri(request.Scheme + "://" + request.Host.Value), url.Content(contentPath)).ToString();
        }

        public static string AbsoluteAction(this IUrlHelper url, string action, string controller, object routeValues = null)
        {
            string scheme = url.ActionContext.HttpContext.Request.Scheme;
            return url.Action(action, controller, routeValues, scheme);
        }
    }
}
