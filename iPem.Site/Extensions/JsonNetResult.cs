using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Mvc;

namespace iPem.Site.Extensions {
    public class JsonNetResult : JsonResult {
        public DefaultValueHandling DefaultValueHandling { get; set; }

        public NullValueHandling NullValueHandling { get; set; }

        public ReferenceLoopHandling ReferenceLoopHandling { get; set; }

        public DateFormatHandling DateFormatHandling { get; set; }

        public String DateFormatString { get; set; }

        public Formatting Formatting { get; set; }

        public JsonNetResult() {
            this.DefaultValueHandling = DefaultValueHandling.Include;
            this.NullValueHandling = NullValueHandling.Include;
            this.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            this.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            this.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            this.Formatting = Formatting.Indented;
        }

        public JsonNetResult(object data)
            : this() {
            this.Data = data;
        }

        public JsonNetResult(object data, JsonRequestBehavior behavior)
            : this(data) {
            this.JsonRequestBehavior = behavior;
        }

        public override void ExecuteResult(ControllerContext context) {
            if (context == null) {
                throw new ArgumentNullException("context");
            }
            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
                String.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase)) {
                throw new InvalidOperationException("This request has been blocked because sensitive information could be disclosed to third party web sites when this is used in a GET request. To allow GET requests, set JsonRequestBehavior to AllowGet.");
            }

            HttpResponseBase response = context.HttpContext.Response;

            if (!String.IsNullOrEmpty(ContentType)) {
                response.ContentType = ContentType;
            } else {
                //response.ContentType = "application/json";
                response.ContentType = "text/html";
            }
            if (ContentEncoding != null) {
                response.ContentEncoding = ContentEncoding;
            }
            if (Data != null) {
                response.Write(JsonConvert.SerializeObject(Data, new JsonSerializerSettings {
                    DefaultValueHandling = this.DefaultValueHandling,
                    NullValueHandling = this.NullValueHandling,
                    ReferenceLoopHandling = this.ReferenceLoopHandling,
                    DateFormatHandling = this.DateFormatHandling,
                    DateFormatString = this.DateFormatString,
                    Formatting = this.Formatting
                }));
            }
        }
    }
}