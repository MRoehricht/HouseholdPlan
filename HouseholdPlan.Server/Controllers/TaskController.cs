﻿using HouseholdPlan.Core.Models.Work;
using HouseholdPlan.Data.Entities.Work;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HouseholdPlan.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        // GET: api/<MyTasksController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
