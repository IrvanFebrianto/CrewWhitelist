using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class TaskList
    {
        /**
         * mengembalikan nested list of task
         *      task 1
         *          task 1.1
         *          task 1.2
         * @param excludeId id dan semua subtask nya tidak diambil
         */
        //public List<TaskPresentationStub> MapList(List<task> dbItems, int? parentId = null, int? excludeId = null)
        //{
        //    List<TaskPresentationStub> result = new List<TaskPresentationStub>();
        //    TaskPresentationStub viewModel = null;
        //    List<task> rootTasks = null;

        //    if (dbItems != null)
        //    {
        //        if (parentId == null)
        //            rootTasks = dbItems.Where(m => m.parent_id == null).OrderBy(m => m.menu_order).ToList();
        //        else
        //            rootTasks = dbItems.Where(m => m.parent_id == parentId).OrderBy(m => m.menu_order).ToList();

        //        foreach (var item in rootTasks)
        //        {
        //            if (item.id != excludeId)
        //            {
        //                viewModel = new TaskPresentationStub(item);
        //                viewModel.Subtask = this.MapList(dbItems, item.id, excludeId);
        //                result.Add(viewModel);
        //            }
        //        }
        //    }

        //    return result;
        //}

        ///**
        // * mengembalikan list yang sudah digabung menjadi 1 level
        // * menghapus bobot untuk depth > 0
        // */
        //public List<TaskPresentationStub> FlattenList(List<TaskPresentationStub> orig, string prefix = null, int depth = 0)
        //{
        //    List<TaskPresentationStub> flat = new List<TaskPresentationStub>();
        //    string modPrefix = "";

        //    foreach (var item in orig)
        //    {
        //        if (depth > 0)
        //            item.Weight = null;
        //        item.Level = depth;

        //        if (prefix != null)
        //        {
        //            if (prefix != null)
        //                modPrefix = string.Concat(Enumerable.Repeat(prefix, depth));

        //            item.Title = modPrefix + item.Title;
        //        }
        //        flat.Add(item);
        //        if (item.Subtask != null)
        //        {
        //            if (item.Subtask.Count() > 0)
        //                flat = flat.Concat(FlattenList(item.Subtask, prefix, depth + 1)).ToList();
        //        }
        //    }

        //    return flat;
        //}

        ///**
        // * mengganti duration di orig menjadi duration recovery
        // */
        //public List<TaskPresentationStub> DisplayRecoveryPlan(List<TaskPresentationStub> orig, List<task_recovery> recovery)
        //{
        //    List<TaskPresentationStub> modified = orig; task_recovery taskRecovery = null;

        //    foreach (var item in modified)
        //    {
        //        if (recovery.Where(m => m.task_id == item.Id).Count() > 0)
        //        {
        //            taskRecovery = recovery.Where(m => m.task_id == item.Id).First();
        //            item.Duration = new TaskRecoveryPresentationStub(taskRecovery).Duration;
        //        }
        //        else
        //            item.Duration = "";
        //    }

        //    return modified;
        //}

        ///**
        // * set plan, actual, deviasi, dan status untuk setiap task di dalam list orig
        // * @param taskSchedules list of last schedule untuk setiap task
        // * @param taskProgress list of progress untuk setiap task (di-sum)
        // */
        //public List<TaskPresentationStub> SetStatus(List<TaskPresentationStub> orig, List<task_schedule> taskSchedules, List<task_progress> taskProgresses)
        //{
        //    //kamus
        //    List<TaskPresentationStub> modified = new List<TaskPresentationStub>();
        //    List<task_schedule> projectTaskSchedules;
        //    List<task_progress> projectTaskProgresses;

        //    //algoritma
        //    foreach (TaskPresentationStub item in orig)
        //    {
        //        projectTaskSchedules = taskSchedules.Where(m => m.task_id == item.Id).ToList();
        //        projectTaskProgresses = taskProgresses.Where(m => m.task_id == item.Id).ToList();

        //        item.SetStatus(projectTaskSchedules, projectTaskProgresses);

        //        modified.Add(item);
        //    }

        //    return modified;
        //}
    }
}