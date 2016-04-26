using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business.Entities;

namespace WebUI.Models.Crew
{
    public class CrewAdminPresentationStub
    {
		// Example model value from scaffolder script: 0
        public string barcode{get; set;} 
		public string crewName { get; set; }
        public DateTime assignDate{get;set;}
        public string crewStatus { get; set; }
        public string crewAirport { get; set; }
        public string companyAirways { get; set; }
	//	public DateTime? startDate{get;set;}
	//	public DateTime? endDate{get;set;}
		
		public CrewAdminPresentationStub() { }

		public CrewAdminPresentationStub(crew dbItem) { 
			this.barcode = dbItem.barcode;
			this.crewName = dbItem.crew_name;
			this.assignDate = dbItem.assign_date;
            this.crewStatus = dbItem.crew_status;
            this.crewAirport = dbItem.crew_airport;
            this.companyAirways = dbItem.company_airways;
  //          this.startDate = dbItem.start_date;
//            this.endDate = dbItem.end_date;

		}

		public List<CrewAdminPresentationStub> MapList(List<crew> dbItems)
        {
            List<CrewAdminPresentationStub> retList = new List<CrewAdminPresentationStub>();

            foreach (crew dbItem in dbItems)
                retList.Add(new CrewAdminPresentationStub(dbItem));

            return retList;
        }
	}
}

