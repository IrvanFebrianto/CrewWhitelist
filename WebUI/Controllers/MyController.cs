﻿using Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebUI.Infrastructure;

namespace WebUI.Controllers
{
    //[Authorize]
    //[LogActionFilter]
    public abstract class MyController : Controller
    {
        public ILogRepository RepoLog;

        public MyController(ILogRepository repoLog)
        {
            RepoLog = repoLog;
        }

        #region Http404 handling

        protected override void HandleUnknownAction(string actionName)
        {
            // If controller is ErrorController dont 'nest' exceptions
            if (this.GetType() != typeof(ErrorController))
                this.InvokeHttp404(HttpContext);
        }

        public ActionResult InvokeHttp404(HttpContextBase httpContext)
        {
            IController errorController = DependencyResolver.Current.GetService<ErrorController>();
            var errorRoute = new RouteData();
            errorRoute.Values.Add("controller", "Error");
            errorRoute.Values.Add("action", "Http404");
            errorRoute.Values.Add("url", httpContext.Request.Url.OriginalString);
            errorController.Execute(new RequestContext(
                 httpContext, errorRoute));

            return new EmptyResult();
        }

        #endregion
    }
}
