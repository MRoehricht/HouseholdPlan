using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using HouseholdPlan.Core.Models.Work;
using HouseholdPlan.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace HouseholdPlan.Server.Pages
{
    public class TasksTableModel : PageModel
    {
        public List<HouseholdTask> HouseholdTasks { get; set; } = new List<HouseholdTask>();

        private readonly ILogger<TasksTableModel> _logger;
        private readonly ITaskService _taskService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TasksTableModel(ILogger<TasksTableModel> logger, ITaskService taskService, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _taskService = taskService;
            _httpContextAccessor = httpContextAccessor;


            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var userGuid = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                HouseholdTasks = taskService.GetUserTasks(userGuid);
            }
        }




        public void OnGet()
        {
        }
    }
}
