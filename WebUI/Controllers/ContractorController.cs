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
using WebUI.Models.Contractor;

namespace WebUI.Controllers
{
    public class ContractorController : MyController
    {
		private IContractorRepository RepoContractor;
		private ILogRepository RepoLog;

        public ContractorController(IContractorRepository repoContractor, ILogRepository repoLog)
			: base(repoLog)
        {
            RepoContractor = repoContractor;
        }

		[MvcSiteMapNode(Title = "Contractor", ParentKey = "Dashboard",Key="IndexContractor")]
		[SiteMapTitle("Breadcrumb")]
        public ActionResult Index()
        {
            return View();
        }

        public string Binding()
        {
            //kamus
            GridRequestParameters param = GridRequestParameters.Current;

            List<contractor> items = RepoContractor.FindAll(param.Skip, param.Take, (param.Sortings != null ? param.Sortings.ToList() : null), param.Filters);
            int total = RepoContractor.Count(param.Filters);

            return new JavaScriptSerializer().Serialize(new { total = total, data = new ContractorPresentationStub().MapList(items) });
        }

		[MvcSiteMapNode(Title = "Create", ParentKey = "IndexContractor")]
		[SiteMapTitle("Breadcrumb")]
        public ActionResult Create()
        {
			
            ContractorFormStub formStub = new ContractorFormStub();

            return View("Form", formStub);
        }

        [HttpPost]
		[SiteMapTitle("Breadcrumb")]
        public ActionResult Create(ContractorFormStub model)
        {
            //bool isNameExist = RepoContractor.Find().Where(p => p.name == model.Name).Count() > 0;
            
            if (ModelState.IsValid)
            {
                contractor dbItem = new contractor();
                dbItem = model.GetDbObject(dbItem);

                try
                {
                    RepoContractor.Save(dbItem);
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

		[MvcSiteMapNode(Title = "Edit", ParentKey = "IndexContractor", Key = "EditContractor", PreservedRouteParameters = "id")]
		[SiteMapTitle("Breadcrumb")]
        public ActionResult Edit(int id)
        {
            contractor contractor = RepoContractor.FindByPk(id);
            ContractorFormStub formStub = new ContractorFormStub(contractor);
            return View("Form", formStub);
        }

        [HttpPost]
		[SiteMapTitle("Breadcrumb")]
        public ActionResult Edit(ContractorFormStub model)
        {
            //bool isNameExist = RepoKompetitor.Find().Where(p => p.name == model.Name && p.id != model.Id).Count() > 0;

            if (ModelState.IsValid)
            {
                contractor dbItem = RepoContractor.FindByPk(model.Id);
                dbItem = model.GetDbObject(dbItem);

                try
                {
                    RepoContractor.Save(dbItem);
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
			contractor dbItem = RepoContractor.FindByPk(id);

            RepoContractor.Delete(dbItem);

            return Json(response);
        }
	}
}

