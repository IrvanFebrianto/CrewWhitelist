using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Script.Serialization;
using WebUI.Infrastructure;
using WebUI.Infrastructure.Abstract;
using WebUI.Models;
using WebUI.Controllers;
using WebUI.Extension;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Web.Mvc.Filters;
using Business.Abstract;
using Business.Entities;
using WebUI.Models.Crew;

namespace WebUI.Controllers
{
    public class CrewController : Controller
    {
		private ICrewRepository RepoCrew;
        private ICrewWhitelistRepository RepoCrewWhitelist;
		private ILogRepository RepoLog;
        private crew_whitelistEntities context = new crew_whitelistEntities();

        public CrewController(ICrewRepository repoCrew, ICrewWhitelistRepository repoCrewWhitelist,ILogRepository repoLog)
        {
            RepoCrew = repoCrew;
            RepoCrewWhitelist = repoCrewWhitelist;
            RepoLog = repoLog;
        }

		//[MvcSiteMapNode(Title = "Contractor", ParentKey = "Dashboard",Key="IndexContractor")]
		//[SiteMapTitle("Breadcrumb")]
        [AuthorizeRole(Roles="admin")]
        public ActionResult IndexAdmin()
        {
            return View();
        }

        [AuthorizeRole(Roles = "adminwhitelist")]
        public ActionResult IndexAdminWhitelist()
        {
            return View();
        }

        [AuthorizeRole(Roles = "admin")]
        public string BindingCrew()
        {
            //kamus
            GridRequestParameters param = GridRequestParameters.Current;

            List<crew> items = RepoCrew.FindAll(param.Skip, param.Take, (param.Sortings != null ? param.Sortings.ToList() : null), param.Filters);
            int total = RepoCrew.Count(param.Filters);

            return new JavaScriptSerializer().Serialize(new { total = total, data = new CrewAdminPresentationStub().MapList(items) });
        }

        [AuthorizeRole(Roles = "adminwhitelist")]
        public string BindingCrewWhitelist()
        {
            //kamus
            GridRequestParameters param = GridRequestParameters.Current;

            List<crew_whitelist> items = RepoCrewWhitelist.FindAll(param.Skip, param.Take, (param.Sortings != null ? param.Sortings.ToList() : null), param.Filters);
            int total = RepoCrewWhitelist.Count(param.Filters);

            return new JavaScriptSerializer().Serialize(new { total = total, data = new CrewAdminWhitelistPresentationStub().MapList(items) });
        }

        [AuthorizeRole(Roles = "adminwhitelist")]
        public string BindingCrewWhitelistToday()
        {
            //kamus
            GridRequestParameters param = GridRequestParameters.Current;

            List<crew_whitelist> items = RepoCrewWhitelist.FindAll(param.Skip, param.Take, (param.Sortings != null ? param.Sortings.ToList() : null), param.Filters);
            int total = RepoCrewWhitelist.Count(param.Filters);

            List<crew> crews = new List<crew>();

            foreach(crew_whitelist item in items){
                if (item.start_date >= DateTime.Now && item.end_date <= DateTime.Now && crews.FirstOrDefault(x => x.barcode == item.barcode)==null)
                {
                    crews.Add(context.crews.FirstOrDefault(x => x.barcode == item.barcode));
                }
            }

            return new JavaScriptSerializer().Serialize(new { total = total, data = new CrewAdminWhitelistPresentationStub().MapList(items) });
        }

		//[MvcSiteMapNode(Title = "Create", ParentKey = "IndexContractor")]
		//[SiteMapTitle("Breadcrumb")]
        [AuthorizeRole(Roles = "admin")]
        public ActionResult CreateCrew()
        {
			
            CrewAdminFormStub formStub = new CrewAdminFormStub();

            return View("FormAdmin", formStub);
        }

        [AuthorizeRole(Roles = "admin")]
        [HttpPost]
		//[SiteMapTitle("Breadcrumb")]
        public ActionResult CreateCrew(CrewAdminFormStub model)
        {
            //bool isNameExist = RepoContractor.Find().Where(p => p.name == model.Name).Count() > 0;
            
            //dbItem.id = 1;
            
            if (ModelState.IsValid)
            {
                crew dbItem = new crew();
                dbItem = model.GetDbObject(dbItem);
                dbItem.assign_date = DateTime.Now;
                //dbItem.Id = RepoCrew.Count() + 1;
                dbItem.barcode = dbItem.assign_date.ToString("ddMMyyyy") + "-" + (RepoCrew.Count()+1).ToString("D3");
                //dbItem = model.GetDbObject(dbItem);
                //dbItem.barcode = "Halo";

                try
                {
                    RepoCrew.Save(dbItem);
                }
                catch (Exception e)
                {
                    return View("FormAdmin", model);
                }

                //message
                string template = HttpContext.GetGlobalResourceObject("MyGlobalMessage", "CreateSuccess").ToString();
                this.SetMessage(model.crewName, template);
                //x++;


                return RedirectToAction("IndexAdmin");
            }
            else
            {
                return View("FormAdmin", model);
            }
        }

		//[MvcSiteMapNode(Title = "Edit", ParentKey = "IndexContractor", Key = "EditContractor", PreservedRouteParameters = "id")]
		//[SiteMapTitle("Breadcrumb")]
        [AuthorizeRole(Roles = "admin")]
        public ActionResult EditCrew(string barcode)
        {
            crew crew = RepoCrew.FindByPk(barcode);
            CrewAdminFormStub FormAdminStub = new CrewAdminFormStub(crew);
            return View("FormAdmin", FormAdminStub);
        }

        [AuthorizeRole(Roles = "admin")]
        [HttpPost]
		//[SiteMapTitle("Breadcrumb")]
        public ActionResult EditCrew(CrewAdminFormStub model)
        {
            //bool isNameExist = RepoKompetitor.Find().Where(p => p.name == model.Name && p.id != model.Id).Count() > 0;

            if (ModelState.IsValid)
            {
                crew dbItem = RepoCrew.FindByPk(model.barcode);
                dbItem = model.GetDbObject(dbItem);
                //dbItem.assign_date = DateTime.Now;
                //dbItem.barcode = dbItem.assign_date.ToString("ddmmyyyy") + "-" + (RepoCrew.Count() + 1.ToString("D3"));
                try
                {
                    RepoCrew.Save(dbItem);
                }
                catch (Exception e)
                { 
                    return View("FormAdmin", model);
                }

                //message
                string template = HttpContext.GetGlobalResourceObject("MyGlobalMessage", "CreateSuccess").ToString();
                this.SetMessage(model.crewName, template);

                return RedirectToAction("IndexAdmin");
            }
            else
            {
                return View("FormAdmin", model);
            }
        }

        [AuthorizeRole(Roles = "admin")]
		[HttpPost]
		public JsonResult DeleteCrew(string barcode)
        {
			string template = "";
			ResponseModel response = new ResponseModel(true);
			crew dbItem = RepoCrew.FindByPk(barcode);

            RepoCrew.Delete(dbItem);

            return Json(response);
        }

        [AuthorizeRole(Roles = "adminwhitelist")]
        public ActionResult CreateCrewWhitelist()
        {

            CrewAdminWhitelistFormStub formStub = new CrewAdminWhitelistFormStub();

            return View("FormAdminWhitelist", formStub);
        }

        [AuthorizeRole(Roles = "adminwhitelist")]
        [HttpPost]
        //[SiteMapTitle("Breadcrumb")]
        public ActionResult CreateCrewWhitelist(CrewAdminWhitelistFormStub model)
        {
            //bool isNameExist = RepoContractor.Find().Where(p => p.name == model.Name).Count() > 0;

            if (ModelState.IsValid)
            {
                crew_whitelist dbItem = new crew_whitelist();
                dbItem = model.GetDbObject(dbItem);

                try
                {
                    RepoCrewWhitelist.Save(dbItem);
                }
                catch (Exception e)
                {
                    return View("FormAdminWhitelist", model);
                }

                //message
                string template = HttpContext.GetGlobalResourceObject("MyGlobalMessage", "CreateSuccess").ToString();
                this.SetMessage("Error", template);

                return RedirectToAction("IndexAdminWhitelist");
            }
            else
            {
                return View("FormAdminWhitelist", model);
            }
        }

        //[MvcSiteMapNode(Title = "Edit", ParentKey = "IndexAdminWhitelistContractor", Key = "EditContractor", PreservedRouteParameters = "id")]
        //[SiteMapTitle("Breadcrumb")]
        [AuthorizeRole(Roles = "adminwhitelist")]
        public ActionResult EditCrewWhitelist(int id)
        {
            crew_whitelist crew = RepoCrewWhitelist.FindByPk(id);
            CrewAdminWhitelistFormStub FormAdminWhitelistStub = new CrewAdminWhitelistFormStub(crew);
            return View("FormAdminWhitelist", FormAdminWhitelistStub);
        }

        [AuthorizeRole(Roles = "adminwhitelist")]
        [HttpPost]
        //[SiteMapTitle("Breadcrumb")]
        public ActionResult EditCrewWhitelist(CrewAdminWhitelistFormStub model)
        {
            //bool isNameExist = RepoKompetitor.Find().Where(p => p.name == model.Name && p.id != model.Id).Count() > 0;

            if (ModelState.IsValid)
            {
                crew_whitelist dbItem = RepoCrewWhitelist.FindByPk(model.id);
                dbItem = model.GetDbObject(dbItem);

                try
                {
                    RepoCrewWhitelist.Save(dbItem);
                }
                catch (Exception e)
                {
                    return View("FormAdminWhitelist", model);
                }

                //message
                string template = HttpContext.GetGlobalResourceObject("MyGlobalMessage", "CreateSuccess").ToString();
                this.SetMessage("Error", template);

                return RedirectToAction("IndexAdminWhitelist");
            }
            else
            {
                return View("FormAdminWhitelist", model);
            }
        }

        [AuthorizeRole(Roles = "adminwhitelist")]
        [HttpPost]
        public JsonResult DeleteCrewWhitelist(int id)
        {
            string template = "";
            ResponseModel response = new ResponseModel(true);
            crew_whitelist dbItem = RepoCrewWhitelist.FindByPk(id);

            RepoCrewWhitelist.Delete(dbItem);

            return Json(response);
        }

        //[AllowAnonymous]
        //public ActionResult Login()
        //{
        //    return View();
        //}

        //[AllowAnonymous]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login(LoginModel u)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using (crew_whitelistEntities user = new crew_whitelistEntities())
        //        {
        //            var v = user.user_role.Where(a => a.username.Equals(u.Username) && a.password.Equals(u.Password)).FirstOrDefault();
        //            if (v != null)
        //            {
        //                Session["logedUserID"] = v.id.ToString();
        //                Session["logedUserName"] = v.username.ToString();
        //                if (v.role.Equals("admin"))
        //                {
        //                    //return View("IndexAdmin");
        //                    return RedirectToAction("IndexAdmin");
        //                }
        //                else
        //                {
        //                    //return View("IndexAdminWhitelist");
        //                    return RedirectToAction("IndexAdminWhitelist");
        //                }
        //            }

        //        }
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "Login Failed");

        //    }
        //    return View();

        //}

        //[AllowAnonymous]
        //public ActionResult LogOut()
        //{
        //    return View("Login");
        //}

	}
}

