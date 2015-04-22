using System;
using Newtonsoft.Json;

namespace System.Web.Mvc
{
    public static class HtmlHelperJson 
    {
        private static readonly JsonSerializerSettings settings;

        static HtmlHelperJson()
        {
            settings = new JsonSerializerSettings();
            settings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
        }

        public static MvcHtmlString ToJson(this HtmlHelper html, object value)
        {
            return MvcHtmlString.Create(JsonConvert.SerializeObject(value, Formatting.None, settings));
        }

       


    }
}