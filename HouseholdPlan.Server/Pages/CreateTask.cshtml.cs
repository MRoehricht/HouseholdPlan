using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseholdPlan.Core.Models.Work;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HouseholdPlan.Server.Pages
{
    public class CreateTaskModel : PageModel
    {

        public HouseholdTask HouseholdTask { get; set; }


        public void OnGet()
        {
        }
    }
}
