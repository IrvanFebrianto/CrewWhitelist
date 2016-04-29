using System;
using Business.Abstract;
using Business.Entities;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebUI.Infrastructure.Abstract;
using WebUI.Models;
using WebUI.Extension;

namespace WebUI.Controllers
{
    public class UserController : Controller
    {
        private IAuthProvider auth;
        //private IUserRepository RepoUser;
        private crew_whitelistEntities context = new crew_whitelistEntities();
        public UserController(IAuthProvider authParam)
        {
            auth = authParam;
            //this.RepoUser = repouser;
        }

        [AllowAnonymous]
        public ActionResult Login(string ReturnUrl = null)
        {
            if (Request.IsAuthenticated){
                if (User.IsInRole("admin"))
                {
                    return RedirectToAction("IndexAdmin", "Crew");
                }
                else if (User.IsInRole("adminwhitelist"))
                {
                    return RedirectToAction("IndexAdminWhitelist", "Crew");
                }
                return View();
            }
                
            else
            {
                if (ReturnUrl != null)
                    ViewBag.ReturnUrl = ReturnUrl;
                return View();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                //if (auth.Authenticate(model.Username, model.Password))
                //{
                    Session["username"] = model.Username;
                    var role = context.user_role.Where(x => x.username == model.Username && x.password == model.Password)
                                    .Select(x => x.role)
                                    .FirstOrDefault();

                    string roles = "";
                    if (role == "admin")
                    {
                        roles = "admin";
                    }
                    else
                    {
                        roles = "adminwhitelist";
                    }

                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                        1,
                        model.Username,  //user id
                        DateTime.Now,
                        DateTime.Now.AddMinutes(60),  // expiry
                        false,  //do not remember
                        roles,
                        "/");
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                                                       FormsAuthentication.Encrypt(authTicket))
                                                       {
                                                           HttpOnly = true,
                                                           Expires = authTicket.Expiration
                                                       };
                    //Response.Cookies.Add(cookie);
                    Response.SetCookie(cookie);
                    if (role =="admin")
                    {
                        return RedirectToAction("IndexAdmin", "Crew");
                    }
                    else if (role =="adminwhitelist")
                    {
                        return RedirectToAction("IndexAdminWhitelist", "Crew");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Username or password is incorrect");
                    }
                
            }
            return View();
        }

        /// <summary>
        /// clear session
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session["username"] = null;
            FormsAuthentication.SignOut();
            return Redirect(FormsAuthentication.LoginUrl);
        }

        
    }
}
