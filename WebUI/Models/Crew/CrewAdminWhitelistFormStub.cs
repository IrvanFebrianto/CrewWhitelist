using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business.Entities;

namespace WebUI.Models.Crew
{
    public class CrewAdminWhitelistFormStub
    {
        public string barcode { get; set; }
        public int id { get; set; }
        //public string crewName { get; set; }
        //public DateTime assignDate = DateTime.Now;
        //public string crewStatus { get; set; }
        //public string crewAirport { get; set; }
        //public string companyAirways { get; set; }
		public DateTime startDate { get; set; }
		public DateTime endDate { get; set; }

		public CrewAdminWhitelistFormStub() { }


		public CrewAdminWhitelistFormStub(crew_whitelist dbItem)
			: this()
		{
            this.barcode = dbItem.barcode;
            this.id = dbItem.Id;
            //this.crewName = dbItem.crew_name;
            //this.assignDate = dbItem.assign_date;
            //this.crewStatus = dbItem.crew_status;
            //this.crewAirport = dbItem.crew_airport;
            //this.companyAirways = dbItem.company_airways;
            this.startDate = dbItem.start_date;
            this.endDate = dbItem.end_date;
		}

		public crew_whitelist GetDbObject(crew_whitelist dbItem) {
            dbItem.barcode = this.barcode;
            dbItem.Id = this.id;
            //dbItem.crew_name = this.crewName;
            //dbItem.assign_date = this.assignDate;
            //dbItem.crew_status = this.crewStatus;
            //dbItem.crew_airport = this.crewAirport;
            //dbItem.company_airways = this.companyAirways;
            dbItem.start_date = this.startDate;
            dbItem.end_date = this.endDate;
			return dbItem;
		}

		#region options


		#endregion

	}
}

