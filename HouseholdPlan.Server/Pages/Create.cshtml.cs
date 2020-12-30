using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseholdPlan.Core.Models.Work;
using HouseholdPlan.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HouseholdPlan.Server.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ITaskService taskService;

        [BindProperty]
        public HouseholdTask HouseholdTask { get; set; } = new HouseholdTask();

        public CreateModel(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            taskService.CreateOrUpdateTask(HouseholdTask);

            return Page();//  RedirectToPage("./Index");
        }


        public void OnGet()
        {
        }
    }
}
