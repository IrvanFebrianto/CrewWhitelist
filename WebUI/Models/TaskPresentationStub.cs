using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Enums;

namespace WebUI.Models
{
    public class TaskPresentationStub
    {
        //public int Id { get; set; }
        //public int ProjectId { get; set; }
        //public string Title { get; set; }
        //public string Weight { get; set; }
        //public string StartDate { get; set; } //2014-01-31
        //public string FinishDate { get; set; }
        //public string Duration { get; set; }
        //public int Level { get; set; } //level dari 0, di set di luar kelas
        //public List<TaskPresentationStub> Subtask { get; set; }

        ////status related
        //public double WeightInt;
        //public double PlanAbs { get; set; } //nilai plan non-relatif terhadap weight
        //public double Plan { get; set; } //nilei plan relatif terhadap weight
        //public double ActualAbs { get; set; } //nilai actual non-relatif terhadap weight
        //public double Actual { get; set; } //nilai actual relatif terhadap weight
        //public double DeviationAbs { get; set; }
        //public double Deviation { get; set; }
        //public ProjectStatus Status { get; set; }

        //public string StatusImage
        //{
        //    get
        //    {
        //        string stringUrl = "",
        //            image = "";

        //        switch (this.Status)
        //        {
        //            case ProjectStatus.NOT_STARTED:
        //                stringUrl = "~/Content/traffic-light/white.jpg";
        //                break;
        //            case ProjectStatus.ON_TRACK:
        //                stringUrl = "~/Content/traffic-light/green.jpg";
        //                break;
        //            case ProjectStatus.NOT_ON_TRACK:
        //                stringUrl = "~/Content/traffic-light/yellow.jpg";
        //                break;
        //            case ProjectStatus.DELAY:
        //                stringUrl = "~/Content/traffic-light/red.jpg";
        //                break;
        //            case ProjectStatus.COMPLETED:
        //                stringUrl = "~/Content/traffic-light/blue.jpg";
        //                break;
        //        }

        //        var url = VirtualPathUtility.ToAbsolute(stringUrl);
        //        image = "<img src=\"" + url + "\" />";
        //        return image;
        //    }
        //}

        //public TaskPresentationStub(task dbItem)
        //{
        //    this.Id = dbItem.id;
        //    this.ProjectId = dbItem.project_id;
        //    this.Title = dbItem.title;
        //    if (dbItem.weight != null)
        //    {
        //        this.WeightInt = dbItem.weight.Value;
        //        this.Weight = dbItem.weight.Value.ToString() + "%";
        //    }
        //    this.StartDate = dbItem.start_date.ToString("d MMMM yyyy");
        //    this.FinishDate = dbItem.finish_date.ToString("d MMMM yyyy");
        //    //this.Duration = (dbItem.finish_date - dbItem.start_date).TotalDays.ToString() + " hari<br>" + this.StartDate + " s/d " + this.FinishDate;
        //    this.Duration = ((dbItem.finish_date - dbItem.start_date).TotalDays + 1).ToString() + " hari (" + dbItem.start_date.ToString("M/d/yy") + " s/d " + dbItem.finish_date.ToString("M/d/yy") + ")";
        //    //this.Duration = (dbItem.finish_date - dbItem.start_date).TotalDays.ToString() + " hari";
        //}

        ///**
        // * menentukan status dari task
        // *      not started     : taskSchedules = 0
        // *      completed       : sum(taskProgresses) = 100
        // *      delay           : sum(taskProgress) - (taskSchedules.last * task.weight) < -1
        // *      not on track    : 0 < (sum(taskProgress) - (taskSchedules.last * task.weight)) <= 1
        // *      on track        : (sum(taskProgress) - (taskSchedules.last * task.weight)) > 0
        // * asumsi semua task_progress.weekly_report.start_date < now
        // */
        //public void SetStatus(List<task_schedule> taskSchedules, List<task_progress> taskProgresses)
        //{
        //    if (taskSchedules.Count() == 0)
        //    {
        //        Status = ProjectStatus.NOT_STARTED;
        //    }
        //    else
        //    {
        //        //calculate plan, ambil task schedule terakhir
        //        PlanAbs = 0;
        //        task_schedule taskSchedule = taskSchedules.Where(m => m.task_id == this.Id).OrderByDescending(m => m.finish_date).FirstOrDefault();
        //        if (taskSchedule != null)
        //        {
        //            PlanAbs += taskSchedule.progress;
        //        }

        //        //calculate actual, jumlahkan task progress
        //        ActualAbs = taskProgresses.Sum(m => (double?)m.week_progress) ?? 0;

        //        //deviation
        //        Plan = PlanAbs * WeightInt / 100;
        //        Actual = ActualAbs * WeightInt / 100;
        //        DeviationAbs = ActualAbs - PlanAbs;
        //        Deviation = Actual - Plan;

        //        if (Actual >= 100)
        //            Status = ProjectStatus.COMPLETED;
        //        else if (DeviationAbs < -1)
        //            Status = ProjectStatus.DELAY;
        //        else if (DeviationAbs < 0)
        //            Status = ProjectStatus.NOT_ON_TRACK;
        //        else
        //            Status = ProjectStatus.ON_TRACK;
        //    }
        //}
    }
}