using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HouseholdPlan.Core.Models.Work;
using HouseholdPlan.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HouseholdPlan.Server.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ITaskService taskService;
        private readonly IHttpContextAccessor httpContextAccessor;

        [BindProperty]
        public HouseholdTask HouseholdTask { get; set; } = new HouseholdTask();

        public CreateModel(ITaskService taskService, IHttpContextAccessor httpContextAccessor)
        {
            this.taskService = taskService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            if (httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var userGuid = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                HouseholdTask.CreatorId = userGuid;

                taskService.CreateOrUpdateTask(HouseholdTask);
            }



            return Page();//  RedirectToPage("./Index");
        }


        public void OnGet()
        {
        }
    }
}

