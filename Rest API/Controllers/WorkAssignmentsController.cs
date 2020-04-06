using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimesheetRestApi.Models;

namespace TimesheetRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkAssignmentsController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public List<WorkAssignments> GetAll()
        {
            tuntidbContext context = new tuntidbContext();

            List<WorkAssignments> works = context.WorkAssignments.ToList();

            return works;
        }
    }
}