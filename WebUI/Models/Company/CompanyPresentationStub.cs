using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business.Entities;

namespace WebUI.Models.Company
{
    public class CompanyPresentationStub
    {
		// Example model value from scaffolder script: 0
		public int Id { get; set; }
		public string Name { get; set; }
		
		public CompanyPresentationStub() { }

		public CompanyPresentationStub(company dbItem) { 
			this.Id = dbItem.id;
			this.Name = dbItem.name;
		}

		public List<CompanyPresentationStub> MapList(List<company> dbItems)
        {
            List<CompanyPresentationStub> retList = new List<CompanyPresentationStub>();

            foreach (company dbItem in dbItems)
                retList.Add(new CompanyPresentationStub(dbItem));

            return retList;
        }
	}
}

