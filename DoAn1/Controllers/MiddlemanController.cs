using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using Microsoft.AspNetCore.Http;

namespace DoAn1.Controllers
{
    public class MiddlemanController : Controller
    {
        // GET: Middleman
        //public HttpSessionState SharedSession
        //{
        //    get
        //    {
        //        return System.Web.HttpContext.Current.Session;
        //    }
        //}

    }
    //public class SessionAuthorizeAttribute : AuthorizeAttribute
    //{
    //    protected override bool AuthorizeCore(HttpContextBase httpContext)
    //    {
    //        var middle = new MiddlemanController();
    //        return (string)middle.SharedSession["admin@gmail.com"] == "admin@gmail.com";
    //    }

    //    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    //    {
    //        filterContext.Result = new RedirectToRouteResult(
    //                          new RouteValueDictionary
    //                          (new { action = "Index", controller = "Login" }));
    //    }
    //}
}
