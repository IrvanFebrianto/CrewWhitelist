using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business.Entities;

namespace WebUI.Models.Contractor
{
    public class ContractorPresentationStub
    {
		// Example model value from scaffolder script: 0
		public int Id { get; set; }
		public string Name { get; set; }
		public bool Deleted { get; set; }
		public string ContractorType { get; set; }
		
		public ContractorPresentationStub() { }

		public ContractorPresentationStub(contractor dbItem) { 
			this.Id = dbItem.id;
			this.Name = dbItem.name;
			this.Deleted = dbItem.deleted;
			this.ContractorType = dbItem.contractor_type;
		}

		public List<ContractorPresentationStub> MapList(List<contractor> dbItems)
        {
            List<ContractorPresentationStub> retList = new List<ContractorPresentationStub>();

            foreach (contractor dbItem in dbItems)
                retList.Add(new ContractorPresentationStub(dbItem));

            return retList;
        }
	}
}

