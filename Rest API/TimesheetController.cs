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
    public class TimesheetController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public List<Timesheet> GetAll()
        {
            tuntidbContext context = new tuntidbContext();

            List<Timesheet> timesheet = context.Timesheet.ToList();

            return timesheet;
        }
    }
}