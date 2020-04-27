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
        public string[] GetAll()
        {
            string[] assignmentNames = null;
            tuntidbContext context = new tuntidbContext();

            assignmentNames = (from wa in context.WorkAssignments
                               where (wa.Active == true)
                               select wa.Title).ToArray();

            return assignmentNames;
        }
    }
}
