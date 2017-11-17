using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace iPem.Site {
    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //Remove the XML formatter
            //config.Formatters.Remove(config.Formatters.XmlFormatter);

            //Remove the JSON formatter
            //config.Formatters.Remove(config.Formatters.JsonFormatter);

            //删除默认的XML数据返回格式
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            //通过参数设置数据返回格式
            config.Formatters.JsonFormatter.MediaTypeMappings.Add(new QueryStringMapping("output", "json", "application/json"));
            config.Formatters.XmlFormatter.MediaTypeMappings.Add(new QueryStringMapping("output", "xml", "application/xml"));

            //解决序列化对象属性包含k__BackingField的问题
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new DefaultContractResolver() { IgnoreSerializableAttribute = true };

            //解决序列化对象属性包含k__BackingField的问题
            config.Formatters.XmlFormatter.UseXmlSerializer = true;
        }
    }
}
