using System;
using System.Web.Mvc;

namespace iPem.Site.Extensions {
    public class AjaxAuthorizeAttribute : AuthorizeAttribute {
        protected override void HandleUnauthorizedRequest(AuthorizationContext context) {
            if(context.HttpContext.Request.IsAjaxRequest()) {
                var urlHelper = new UrlHelper(context.RequestContext);
                context.HttpContext.Response.StatusCode = 400;
                context.Result = new JsonResult {
                    Data = new {
                        Error = "NotAuthorized",
                        LoginUrl = urlHelper.Action("Login", "Account", new { ReturnUrl = context.HttpContext.Request.UrlReferrer.PathAndQuery })
                    },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            } else {
                base.HandleUnauthorizedRequest(context);
            }
        }
    }
}