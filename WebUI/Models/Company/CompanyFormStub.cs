using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business.Entities;

namespace WebUI.Models.Company
{
    public class CompanyFormStub
    {
		// Example model value from scaffolder script: 0
		[DisplayName("Id")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
		public int Id { get; set; }

		[DisplayName("Name")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
		public string Name { get; set; }

		public CompanyFormStub() { }


		public CompanyFormStub(company dbItem)
			: this()
		{
			this.Id = dbItem.id;
			this.Name = dbItem.name;
		}

		public company GetDbObject(company dbItem) {
			dbItem.id = this.Id;
			dbItem.name = this.Name;
			return dbItem;
		}

		#region options


		#endregion

	}
}

