using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business.Entities;

namespace WebUI.Models.Contractor
{
    public class ContractorFormStub
    {
		// Example model value from scaffolder script: 0
		[DisplayName("Id")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
		public int Id { get; set; }

		[DisplayName("Name")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
		public string Name { get; set; }

		[DisplayName("Deleted")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
		public bool Deleted { get; set; }

		[DisplayName("Contractor Type")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
		public string ContractorType { get; set; }

		public ContractorFormStub() { }


		public ContractorFormStub(contractor dbItem)
			: this()
		{
			this.Id = dbItem.id;
			this.Name = dbItem.name;
			this.Deleted = dbItem.deleted;
			this.ContractorType = dbItem.contractor_type;
		}

		public contractor GetDbObject(contractor dbItem) {
			dbItem.id = this.Id;
			dbItem.name = this.Name;
			dbItem.deleted = this.Deleted;
			dbItem.contractor_type = this.ContractorType;
			return dbItem;
		}

		#region options


		#endregion

	}
}

