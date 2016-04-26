using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business.Entities;

namespace WebUI.Models.MProject
{
    public class MProjectPresentationStub
    {
		// Example model value from scaffolder script: 0
		public int Id { get; set; }
		public string Name { get; set; }
		public int? ContractorId { get; set; }
		public string Photo { get; set; }
		public string Description { get; set; }
		public System.DateTime StartDate { get; set; }
		public System.DateTime FinishDate { get; set; }
		public bool? Highlight { get; set; }
		public string ProjectStage { get; set; }
		public byte? Status { get; set; }
		public double? Budget { get; set; }
		public string Currency { get; set; }
		public int? Num { get; set; }
		public int? PmcId { get; set; }
		public string Summary { get; set; }
		public int? CompanyId { get; set; }
		public string StatusNonTechnical { get; set; }
		public bool IsCompleted { get; set; }
		public System.DateTime? CompletedDate { get; set; }
		public int ProjectId { get; set; }
		public System.DateTime SubmitForApprovalTime { get; set; }
		public string ApprovalStatus { get; set; }
		public System.DateTime? ApprovalTime { get; set; }
		public bool Deleted { get; set; }
		public string ApprovalMessage { get; set; }
		public string StatusTechnical { get; set; }
		public string ScurveData { get; set; }
		public string CompanyName { get; set; }
		public string ContractorName { get; set; }
		public string ProjectName { get; set; }
		
		public MProjectPresentationStub() { }

		public MProjectPresentationStub(m_project dbItem) { 
			this.Id = dbItem.id;
			this.Name = dbItem.name;
			this.ContractorId = dbItem.contractor_id;
			this.Photo = dbItem.photo;
			this.Description = dbItem.description;
			this.StartDate = dbItem.start_date;
			this.FinishDate = dbItem.finish_date;
			this.Highlight = dbItem.highlight;
			this.ProjectStage = dbItem.project_stage;
			this.Status = dbItem.status;
			this.Budget = dbItem.budget;
			this.Currency = dbItem.currency;
			this.Num = dbItem.num;
			this.PmcId = dbItem.pmc_id;
			this.Summary = dbItem.summary;
			this.CompanyId = dbItem.company_id;
			this.StatusNonTechnical = dbItem.status_non_technical;
			this.IsCompleted = dbItem.is_completed;
			this.CompletedDate = dbItem.completed_date;
			this.ProjectId = dbItem.project_id;
			this.SubmitForApprovalTime = dbItem.submit_for_approval_time;
			this.ApprovalStatus = dbItem.approval_status;
			this.ApprovalTime = dbItem.approval_time;
			this.Deleted = dbItem.deleted;
			this.ApprovalMessage = dbItem.approval_message;
			this.StatusTechnical = dbItem.status_technical;
			this.ScurveData = dbItem.scurve_data;
			this.CompanyName = dbItem.company != null ? dbItem.company.name : "";
			this.ContractorName = dbItem.contractor != null ? dbItem.contractor.name : "";
			this.ProjectName = dbItem.project != null ? dbItem.project.name : "";
		}

		public List<MProjectPresentationStub> MapList(List<m_project> dbItems)
        {
            List<MProjectPresentationStub> retList = new List<MProjectPresentationStub>();

            foreach (m_project dbItem in dbItems)
                retList.Add(new MProjectPresentationStub(dbItem));

            return retList;
        }
	}
}

