using HouseholdPlan.Data.Constants;
using HouseholdPlan.Data.Entities.Work;
using NUnit.Framework;
using System;

namespace HouseholdPlan.Data.Test.Entities.Work
{
    public class TaskHistoryTest
    {
        [Test]
        public void InitTest()
        {
            int id = 1;
            int taskId = 2;
            int editorId = 3;
            DateTime date = DateTime.Now;

            HistoryTaskEntity taskHistory = new HistoryTaskEntity
            {
                Id = id,
                Date = date,
                EditorId = editorId,
                TaskId = taskId,
                Status = ProcessingStatus.InProgress
            };

            Assert.IsNotNull(taskHistory);

            Assert.AreEqual(id, taskHistory.Id);
            Assert.AreEqual(taskId, taskHistory.TaskId);
            Assert.AreEqual(editorId, taskHistory.EditorId);
            Assert.AreEqual(date, taskHistory.Date);
            Assert.AreEqual(ProcessingStatus.InProgress, taskHistory.Status);
        }
    }
}