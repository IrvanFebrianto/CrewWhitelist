using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebUI.Models.Crew
{
    public class CrewAdminFormStub
    {
        //[DisplayName("Barcode")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        public string barcode { get; set; }

        public string crewName { get; set; }

        //[DisplayName("Assign Date")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        public DateTime assignDate { get; set; }

        //[DisplayName("Crew Status")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        public string crewStatus { get; set; }

        //[DisplayName("Crew Airport")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        public string crewAirport { get; set; }

        //[DisplayName("Company Airways")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        public string companyAirways { get; set; }

        //[DisplayName("Start Date")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        //public DateTime startDate { get; set; }

        //[DisplayName("End Date")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        //public DateTime? endDate { get; set; }

        public CrewAdminFormStub() { }


        public CrewAdminFormStub(crew dbItem)
            : this()
        {
            
            
            this.barcode = dbItem.barcode; 
            this.crewName = dbItem.crew_name;
            this.assignDate = dbItem.assign_date;
            this.crewStatus = dbItem.crew_status;
            this.crewAirport = dbItem.crew_airport;
            this.companyAirways = dbItem.company_airways;
        }

        public crew GetDbObject(crew dbItem)
        {
            
            dbItem.crew_name = this.crewName;
            dbItem.assign_date = this.assignDate;
            dbItem.barcode = this.barcode;
            dbItem.crew_status = this.crewStatus;
            dbItem.crew_airport = this.crewAirport;
            dbItem.company_airways = this.companyAirways;
            return dbItem;
        }

        #region options


        #endregion

    }
}

