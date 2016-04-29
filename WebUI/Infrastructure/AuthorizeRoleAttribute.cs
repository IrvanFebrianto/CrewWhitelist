using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Business.Entities;
using WebUI.Models;
using WebUI.Infrastructure.Concrete;
using System.Security.Principal;

namespace WebUI.Infrastructure
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizeRoleAttribute : AuthorizeAttribute
    {
        // list of module names, separated by ',' 
        // i.e. "Technology Management Home, Technology Mapping", user with module access Technology Management Home or Technology Mapping can access page
        crew_whitelistEntities context = new crew_whitelistEntities();
        private readonly string[] allowedrole;
        public string ModuleName { get; set; }

        public AuthorizeRoleAttribute(params string[] role)
        {
            this.allowedrole = role;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string cookieName = FormsAuthentication.FormsCookieName;

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated ||
                filterContext.HttpContext.Request.Cookies == null ||
                filterContext.HttpContext.Request.Cookies[cookieName] == null
            )
            {
                HandleUnauthorizedRequest(filterContext);
                return;
            }

            var authCookie = filterContext.HttpContext.Request.Cookies[cookieName];
            var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            string[] roles = authTicket.UserData.Split(',');

            var userIdentity = new GenericIdentity(authTicket.Name);
            var userPrincipal = new GenericPrincipal(userIdentity, roles);

            filterContext.HttpContext.User = userPrincipal;
            base.OnAuthorization(filterContext);
            //base.OnAuthorization(filterContext);

            //if (filterContext.HttpContext.Request.IsAuthenticated)
            //{
            //    //kamus
            //    List<ModuleAction> moduleAccess;
            //    List<string> moduleNameList;
            //    bool hasAccess = true;

            //    //algoritma
            //    moduleAccess = (HttpContext.Current.User as CustomPrincipal).Modules;

            //    if (ModuleName != null)
            //    {
            //        moduleNameList = ModuleName.Split(',').Select(email => email.Trim()).ToList();
            //        if (moduleAccess.Where(m => moduleNameList.Contains(m.ModuleName)).Count() == 0)
            //        {
            //            hasAccess = false;
            //        }
            //    }

            //    if (!hasAccess)
            //    {
            //       filterContext.Result = new RedirectToRouteResult(new
            //            RouteValueDictionary(new { controller = "Error", action = "Http401", area = "", url = filterContext.HttpContext.Request.Url.OriginalString }));
            //    }
            //}
        }
    }
}