using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Business.Abstract;
using WebUI.Controllers;
using Moq;
using WebUI.Models;
using System.Web.Mvc;
using System.Linq;
using Business.Infrastructure;
using Business.Entities;
using System.Collections.Generic;
using Common.Enums;

namespace SPBD.UnitTests
{
    [TestClass]
    public class TrafficLightTest
    {
        [TestMethod]
        /**
         * Mengecek perhitungan traffic light task
         */
        public void Validate_Tasks_Traffic_Light()
        {
            ////init
            //Mock<ITaskRepository> mockTask = new Mock<ITaskRepository>(); ;
            //Mock<IWeeklyReportRepository> mockWeeklyReport = new Mock<IWeeklyReportRepository>();

            ////dummy data
            ////task
            //mockTask.Setup(m => m.Find(It.IsAny<List<int>>())).Returns(new List<task> 
            //{ 
            //    new task { id = 1, weight = 10 },
            //    new task { id = 2, weight = 20 },
            //    new task { id = 3, weight = 40 },
            //    new task { id = 4, weight = 30 },
            //});

            ////task_schedule / planning
            //mockTask.Setup(m => m.FindLastSchedule(It.IsAny<List<int>>())).Returns(new List<task_schedule>
            //{
            //    new task_schedule { task_id = 1, progress = 40 },
            //    new task_schedule { task_id = 2, progress = 20 },
            //    new task_schedule { task_id = 3, progress = 30 },
            //});

            ////task_progress / actual
            //mockWeeklyReport.Setup(m => m.FindTaskProgress(It.IsAny<List<int>>())).Returns(new List<task_progress>
            //{
            //     new task_progress { task_id = 1, week_progress = 10 },
            //     new task_progress { task_id = 1, week_progress = 10 },
            //     new task_progress { task_id = 1, week_progress = 15 }
            //});

            ////data for check
            //List<int> projectIds = new List<int> { 1 };
            //List<int> taskIds = new List<int> { 11, 12 };
            //TaskList processor = new TaskList();
            //List<TaskPresentationStub> tasks = processor.MapList(mockTask.Object.Find(projectIds));
            //tasks = processor.SetStatus(tasks, mockTask.Object.FindLastSchedule(taskIds), mockWeeklyReport.Object.FindTaskProgress(taskIds));

            ////check
            ////task with schedule & progress
            //TaskPresentationStub task = tasks.Where(m => m.Id == 1).FirstOrDefault();
            //Assert.AreEqual(task.Plan, 4);
            //Assert.AreEqual(task.Actual, 3.5);
            //double checkVal = Math.Abs(task.DeviationAbs - (-5));
            //Assert.IsTrue(checkVal < 0.0000001);
            //checkVal = Math.Abs(task.Deviation - (-0.5));
            //Assert.IsTrue(checkVal < 0.0000001);
            //Assert.AreEqual(task.Status, ProjectStatus.DELAY);

            ////task with schedule & no progress
            //task = tasks.Where(m => m.Id == 2).FirstOrDefault();
            //Assert.AreEqual(task.Plan, 4);
            //Assert.AreEqual(task.Actual, 0);
            //Assert.AreEqual(task.Deviation, -4);
            //Assert.AreEqual(task.Status, ProjectStatus.DELAY);

            ////task with no schedule
            //task = tasks.Where(m => m.Id == 4).FirstOrDefault();
            //Assert.AreEqual(task.Plan, 0);
            //Assert.AreEqual(task.Actual, 0);
            //Assert.AreEqual(task.Deviation, 0);
            //Assert.AreEqual(task.Status, ProjectStatus.NOT_STARTED);
        }
    }
}
