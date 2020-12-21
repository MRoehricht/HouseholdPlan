using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseholdPlan.Core.Models.Work;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace HouseholdPlan.Server.Pages
{
    public class TasksTableModel : PageModel
    {
        public List<HouseholdTask> HouseholdTasks { get; set; }


        private readonly ILogger<TasksTableModel> _logger;

        public TasksTableModel(ILogger<TasksTableModel> logger)
        {
            _logger = logger;
        }



        public void OnGet()
        {
        }
    }
}
