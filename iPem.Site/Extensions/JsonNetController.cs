using System;
using System.Text;
using System.Web.Mvc;

namespace iPem.Site.Extensions {
    public abstract class JsonNetController : Controller {
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior) {
            if (behavior == JsonRequestBehavior.DenyGet &&
                String.Equals(this.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase)) {
                return new JsonResult();
            }

            return new JsonNetResult {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }
    }
}