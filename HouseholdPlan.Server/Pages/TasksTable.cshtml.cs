using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseholdPlan.Core.Models.Work;
using HouseholdPlan.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace HouseholdPlan.Server.Pages
{
    public class TasksTableModel : PageModel
    {
        public List<HouseholdTask> HouseholdTasks { get; set; }

        private readonly ILogger<TasksTableModel> _logger;
        private readonly ITaskService _taskService;

        public TasksTableModel(ILogger<TasksTableModel> logger, ITaskService taskService)
        {
            _logger = logger;
            _taskService = taskService;
            HouseholdTasks = taskService.GetUserTasks(null);
            //HouseholdTasks = new List<HouseholdTask>();
        }




        public void OnGet()
        {
        }
    }
}
