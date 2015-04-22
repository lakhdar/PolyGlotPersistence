using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using System;

namespace System.Web.Mvc
{
    public static class HtmlHelperStaticContent
    {
        public static string StaticContentStorageConnectionString
        {
            get
            {
                return CloudConfigurationManager.GetSetting("StaticContent.StorageConnectionString");
            }
        }
        public static string StaticContentContainer
        {
            get
            {
                return CloudConfigurationManager.GetSetting("StaticContent.Container");
            }
        }
        public static string StaticContentBaseUrl
        {
            get
            {
                var account = CloudStorageAccount.Parse(StaticContentStorageConnectionString);
                return string.Format("{0}/{1}", account.BlobEndpoint.ToString().TrimEnd('/'), StaticContentContainer.TrimStart('/'));
            }
        }

        public static string CdnContentUrl(this UrlHelper url, string contentPath)
        {
            if (contentPath.StartsWith("~"))
            {
                contentPath = contentPath.Substring(1);
            }

            contentPath = string.Format("{0}/{1}", StaticContentBaseUrl.TrimEnd('/'), contentPath.TrimStart('/'));
            return url.Content(contentPath);
        }
    }
}