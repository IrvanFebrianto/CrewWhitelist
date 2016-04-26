using Business.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Enums;
using WebUI.Infrastructure.Validation;

namespace WebUI.Models
{
    public class TaskFormStub
    {
        //public int Id { get; set; }

        //[DisplayName("Uraian")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        //public string Title { get; set; }

        //[Required]
        //public int ProjectId { get; set; }

        //[DisplayName("Tanggal Mulai")]
        //[CheckDay(Day.MONDAY)]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        //public string StartDate { get; set; }

        //public FidItemStatus Status { get; set; }

        //[DisplayName("Tanggal Selesai")]
        //[CheckDay(Day.SUNDAY)]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        //[DateGreater("StartDate")]
        //public string FinishDate { get; set; }

        //[DisplayName("Parent Task")]
        //public int? ParentId { get; set; }

        //[DisplayName("% to overall")]
        //public double? Weight { get; set; }

        //public TaskFormStub() { }

        //public TaskFormStub(task dbItem)
        //{
        //    this.Id = dbItem.id;
        //    this.Title = dbItem.title;
        //    this.ProjectId = dbItem.project_id;
        //    this.ParentId = dbItem.parent_id;
        //    if (dbItem.start_date != null)
        //        this.StartDate = dbItem.start_date.ToString("yyyy-MM-dd");
        //    if (dbItem.finish_date != null)
        //        this.FinishDate = dbItem.finish_date.ToString("yyyy-MM-dd");
        //    if (dbItem.weight != null)
        //        this.Weight = dbItem.weight.Value;
        //}

        //public task GetTask()
        //{
        //    DateTime startDate = DateTime.Parse(this.StartDate);
        //    DateTime finishDate = DateTime.Parse(this.FinishDate);

        //    task dbItem = new task { id = this.Id, title = this.Title, project_id = this.ProjectId, weight = this.Weight, start_date = startDate, finish_date = finishDate, parent_id = this.ParentId };

        //    return dbItem;
        //}
    }
}