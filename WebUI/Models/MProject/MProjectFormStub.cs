using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business.Entities;

namespace WebUI.Models.MProject
{
    public class MProjectFormStub
    {
		// Example model value from scaffolder script: 0
		[DisplayName("Id")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
		public int Id { get; set; }

		[DisplayName("Name")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
		public string Name { get; set; }

		[DisplayName("Contractor Id")]
		public int? ContractorId { get; set; }

		[DisplayName("Photo")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
		public string Photo { get; set; }

		[DisplayName("Description")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
		public string Description { get; set; }

		[DisplayName("Start Date")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
		public System.DateTime StartDate { get; set; }

		[DisplayName("Finish Date")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
		public System.DateTime FinishDate { get; set; }

		[DisplayName("Highlight")]
		public bool? Highlight { get; set; }

		[DisplayName("Project Stage")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
		public string ProjectStage { get; set; }

		[DisplayName("Status")]
		public byte? Status { get; set; }

		[DisplayName("Budget")]
		public double? Budget { get; set; }

		[DisplayName("Currency")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
		public string Currency { get; set; }

		[DisplayName("Num")]
		public int? Num { get; set; }

		[DisplayName("Pmc Id")]
		public int? PmcId { get; set; }

		[DisplayName("Summary")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
		public string Summary { get; set; }

		[DisplayName("Company Id")]
		public int? CompanyId { get; set; }

		[DisplayName("Status Non Technical")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
		public string StatusNonTechnical { get; set; }

		[DisplayName("Is Completed")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
		public bool IsCompleted { get; set; }

		[DisplayName("Completed Date")]
		public System.DateTime? CompletedDate { get; set; }

		[DisplayName("Project Id")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
		public int ProjectId { get; set; }

		[DisplayName("Submit For Approval Time")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
		public System.DateTime SubmitForApprovalTime { get; set; }

		[DisplayName("Approval Status")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
		public string ApprovalStatus { get; set; }

		[DisplayName("Approval Time")]
		public System.DateTime? ApprovalTime { get; set; }

		[DisplayName("Deleted")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
		public bool Deleted { get; set; }

		[DisplayName("Approval Message")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
		public string ApprovalMessage { get; set; }

		[DisplayName("Status Technical")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
		public string StatusTechnical { get; set; }

		[DisplayName("Scurve Data")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
		public string ScurveData { get; set; }

		public List<SelectListItem> CompanyOptions { get; set; }

		public List<SelectListItem> ContractorOptions { get; set; }

		public List<SelectListItem> ProjectOptions { get; set; }

		public MProjectFormStub() { }

		public MProjectFormStub(List<Business.Entities.company> listCompany,List<Business.Entities.contractor> listContractor,List<Business.Entities.project> listProject)
			: this()
		{
			this.FillCompanyOptions(listCompany);
			this.FillContractorOptions(listContractor);
			this.FillProjectOptions(listProject);
		}


		public MProjectFormStub(m_project dbItem,List<Business.Entities.company> listCompany,List<Business.Entities.contractor> listContractor,List<Business.Entities.project> listProject)
			: this(listCompany, listContractor, listProject)
		{
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
		}

		public m_project GetDbObject(m_project dbItem) {
			dbItem.id = this.Id;
			dbItem.name = this.Name;
			dbItem.contractor_id = this.ContractorId;
			dbItem.photo = this.Photo;
			dbItem.description = this.Description;
			dbItem.start_date = this.StartDate;
			dbItem.finish_date = this.FinishDate;
			dbItem.highlight = this.Highlight;
			dbItem.project_stage = this.ProjectStage;
			dbItem.status = this.Status;
			dbItem.budget = this.Budget;
			dbItem.currency = this.Currency;
			dbItem.num = this.Num;
			dbItem.pmc_id = this.PmcId;
			dbItem.summary = this.Summary;
			dbItem.company_id = this.CompanyId;
			dbItem.status_non_technical = this.StatusNonTechnical;
			dbItem.is_completed = this.IsCompleted;
			dbItem.completed_date = this.CompletedDate;
			dbItem.project_id = this.ProjectId;
			dbItem.submit_for_approval_time = this.SubmitForApprovalTime;
			dbItem.approval_status = this.ApprovalStatus;
			dbItem.approval_time = this.ApprovalTime;
			dbItem.deleted = this.Deleted;
			dbItem.approval_message = this.ApprovalMessage;
			dbItem.status_technical = this.StatusTechnical;
			dbItem.scurve_data = this.ScurveData;
			return dbItem;
		}

		#region options

		public void FillCompanyOptions(List<Business.Entities.company> list)
		{
			CompanyOptions = new List<SelectListItem>();
			CompanyOptions.Add(new SelectListItem { Text = "Choose One", Value = "" });
			foreach (Business.Entities.company item in list)
            {
                CompanyOptions.Add(new SelectListItem { Text = item.name, Value = item.id.ToString() });
            }
		
		}
		public void FillContractorOptions(List<Business.Entities.contractor> list)
		{
			ContractorOptions = new List<SelectListItem>();
			ContractorOptions.Add(new SelectListItem { Text = "Choose One", Value = "" });
			foreach (Business.Entities.contractor item in list)
            {
                ContractorOptions.Add(new SelectListItem { Text = item.name, Value = item.id.ToString() });
            }
		
		}
		public void FillProjectOptions(List<Business.Entities.project> list)
		{
			ProjectOptions = new List<SelectListItem>();
			ProjectOptions.Add(new SelectListItem { Text = "Choose One", Value = "" });
			foreach (Business.Entities.project item in list)
            {
                ProjectOptions.Add(new SelectListItem { Text = item.name, Value = item.id.ToString() });
            }
		
		}

		#endregion

	}
}

