using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business.Entities;

namespace WebUI.Models.Crew
{
    public class CrewAdminWhitelistPresentationStub
    {
		// Example model value from scaffolder script: 0
        public string barcode{get; set;} 
		public int id { get; set; }
		//public string crewName { get; set; }
        //public DateTime assignDate{get;set;}
        //public string crewStatus { get; set; }
        //public string crewAirport { get; set; }
        //public string companyAirways { get; set; }
		public DateTime startDate{get;set;}
		public DateTime endDate{get;set;}
		
		public CrewAdminWhitelistPresentationStub() { }

		public CrewAdminWhitelistPresentationStub(crew_whitelist dbItem) { 
			this.barcode = dbItem.barcode;
			this.id = dbItem.Id;
            this.startDate = dbItem.start_date;
            this.endDate = dbItem.end_date;

		}

		public List<CrewAdminWhitelistPresentationStub> MapList(List<crew_whitelist> dbItems)
        {
            List<CrewAdminWhitelistPresentationStub> retList = new List<CrewAdminWhitelistPresentationStub>();

            foreach (crew_whitelist dbItem in dbItems)
                retList.Add(new CrewAdminWhitelistPresentationStub(dbItem));

            return retList;
        }
	}
}

