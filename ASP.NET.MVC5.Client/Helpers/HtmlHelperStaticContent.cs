using System;

namespace System.Web.Mvc
{
    public static class HtmlHelperStaticContent 
    {
        public static string CdnContentUrl(this UrlHelper url, string contentPath)
        {
            return url.Content(contentPath);
        }

    }
}