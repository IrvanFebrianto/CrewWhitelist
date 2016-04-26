using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebUI.Models;
using WebUI.Controllers;
using WebUI.Infrastructure;
using WebUI.Extension;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Web.Mvc.Filters;
using Business.Abstract;
using Business.Entities;
using WebUI.Models.Company;

namespace WebUI.Controllers
{
    public class CompanyController : MyController
    {
		private ICompanyRepository RepoCompany;
		private ILogRepository RepoLog;

        public CompanyController(ICompanyRepository repoCompany, ILogRepository repoLog)
			: base(repoLog)
        {
            RepoCompany = repoCompany;
        }

		[MvcSiteMapNode(Title = "Company", ParentKey = "Dashboard",Key="IndexCompany")]
		[SiteMapTitle("Breadcrumb")]
        public ActionResult Index()
        {
            return View();
        }

        public string Binding()
        {
            //kamus
            GridRequestParameters param = GridRequestParameters.Current;

            List<company> items = RepoCompany.FindAll(param.Skip, param.Take, (param.Sortings != null ? param.Sortings.ToList() : null), param.Filters);
            int total = RepoCompany.Count(param.Filters);

            return new JavaScriptSerializer().Serialize(new { total = total, data = new CompanyPresentationStub().MapList(items) });
        }

		[MvcSiteMapNode(Title = "Create", ParentKey = "IndexCompany")]
		[SiteMapTitle("Breadcrumb")]
        public ActionResult Create()
        {
			
            CompanyFormStub formStub = new CompanyFormStub();

            return View("Form", formStub);
        }

        [HttpPost]
		[SiteMapTitle("Breadcrumb")]
        public ActionResult Create(CompanyFormStub model)
        {
            //bool isNameExist = RepoCompany.Find().Where(p => p.name == model.Name).Count() > 0;
            
            if (ModelState.IsValid)
            {
                company dbItem = new company();
                dbItem = model.GetDbObject(dbItem);

                try
                {
                    RepoCompany.Save(dbItem);
                }
                catch (Exception e)
                {
                    return View("Form", model);
                }

                //message
                string template = HttpContext.GetGlobalResourceObject("MyGlobalMessage", "CreateSuccess").ToString();
                this.SetMessage(model.Name, template);

                return RedirectToAction("Index");
            }
            else
            {
                return View("Form", model);
            }
        }

		[MvcSiteMapNode(Title = "Edit", ParentKey = "IndexCompany", Key = "EditCompany", PreservedRouteParameters = "id")]
		[SiteMapTitle("Breadcrumb")]
        public ActionResult Edit(int id)
        {
            company company = RepoCompany.FindByPk(id);
            CompanyFormStub formStub = new CompanyFormStub(company);
            return View("Form", formStub);
        }

        [HttpPost]
		[SiteMapTitle("Breadcrumb")]
        public ActionResult Edit(CompanyFormStub model)
        {
            //bool isNameExist = RepoKompetitor.Find().Where(p => p.name == model.Name && p.id != model.Id).Count() > 0;

            if (ModelState.IsValid)
            {
                company dbItem = RepoCompany.FindByPk(model.Id);
                dbItem = model.GetDbObject(dbItem);

                try
                {
                    RepoCompany.Save(dbItem);
                }
                catch (Exception e)
                { 
                    return View("Form", model);
                }

                //message
                string template = HttpContext.GetGlobalResourceObject("MyGlobalMessage", "CreateSuccess").ToString();
                this.SetMessage(model.Name, template);

                return RedirectToAction("Index");
            }
            else
            {
                return View("Form", model);
            }
        }

		[HttpPost]
		public JsonResult Delete(int id)
        {
			string template = "";
			ResponseModel response = new ResponseModel(true);
			company dbItem = RepoCompany.FindByPk(id);

            RepoCompany.Delete(dbItem);

            return Json(response);
        }
	}
}

