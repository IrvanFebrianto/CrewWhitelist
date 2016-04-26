using Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Infrastructure;
using WebUI.Extension;
using Business.Entities;
using System.Web.Script.Serialization;
using WebUI.Controllers;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class CurrencyController : MyController
    {
        private ICurrencyRepository RepoCurrency;
        
        public CurrencyController(ICurrencyRepository repoCurrency, ILogRepository repoLog)
            : base(repoLog)
        {
            RepoCurrency = repoCurrency;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            CurrencyFormStub model = new CurrencyFormStub();

            return View("Form", model);
        }

        [HttpPost]
        public ActionResult Create(CurrencyFormStub model)
        {
            if (ModelState.IsValid)
            {
                RepoCurrency.Save(model.GetCurrency());

                //message
                string template = HttpContext.GetGlobalResourceObject("MyGlobalMessage", "CreateSuccess").ToString();
                this.SetMessage(model.Currency, template);

                return RedirectToAction("Index");
            }
            else
            {
                return View("Form", model);
            }
        }

        public ViewResult Edit(int id)
        {
            CurrencyFormStub model = new CurrencyFormStub(RepoCurrency.FindByPk(id));

            ViewBag.name = model.Currency;

            return View("Form", model);
        }

        [HttpPost]
        public ActionResult Edit(CurrencyFormStub model)
        {
            if (ModelState.IsValid)
            {
                RepoCurrency.Save(model.GetCurrency());

                //message
                string template = HttpContext.GetGlobalResourceObject("MyGlobalMessage", "EditSuccess").ToString();
                this.SetMessage(model.Currency, template);

                return RedirectToAction("Index");
            }
            else
            {
                currency dbItem = RepoCurrency.FindByPk(model.Id);
                ViewBag.name = dbItem.currency1;

                return View("Form", model);
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            currency dbItem = RepoCurrency.FindByPk(id);
            ResponseModel response = new ResponseModel(true);
            string message;

            //if (!RepoCurrency.IsAllowDelete(dbItem))
            //{
            //    message = HttpContext.GetGlobalResourceObject("MyGlobalMessage", "DeleteFailedForeignKey").ToString();
            //    response.SetFail(message);
            //}
            //else
            //    RepoCurrency.Delete(dbItem);

            return Json(response);
        }

        #region binding

        public string Binding()
        {
            GridRequestParameters param = GridRequestParameters.Current;

            List<currency> items = RepoCurrency.Find(param.Skip, param.Take, (param.Sortings != null ? param.Sortings.ToList() : null), (param.Filters != null ? param.Filters : null));
            int total = RepoCurrency.Count(param.Filters);

            return new JavaScriptSerializer().Serialize(new { total = total, data = new CurrencyPresentationStub().MapList(items) });
        }

        #endregion
    }
}
