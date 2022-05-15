using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QuanLiSoTietKiem.Security
{
    internal class CheckSessionAttribute : AuthorizeAttribute
    {

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.Session["IDUser"] != null;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                                  new RouteValueDictionary
                                  {
                                   { "action", "Login" },
                                   { "controller", "User" }
                                  });
        }
    }
}