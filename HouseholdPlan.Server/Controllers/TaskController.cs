using HouseholdPlan.Core.Models.Work;
using HouseholdPlan.Data.Entities.Work;
using HouseholdPlan.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HouseholdPlan.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private IHttpContextAccessor httpContextAccessor;
        private ITaskService taskService;

        public TaskController(ITaskService taskService, IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.taskService = taskService;
        }


        // GET: api/<MyTasksController>
        [HttpGet]
        public IEnumerable<HouseholdTask> Get()
        {
            var output = new List<HouseholdTask>();

            if (httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var userGuid = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                output = taskService.GetUserTasks(userGuid);
            }

            return output;
        }

        [HttpPost]
        public ActionResult Post(HouseholdTaskEntity householdTask)
        {

            return new OkResult();
        }

        // GET api/<MyTasksController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        //// POST api/<MyTasksController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/<MyTasksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MyTasksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
