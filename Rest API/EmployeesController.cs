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
    public class EmployeesController : ControllerBase
    {

        // Haetaan kaikki aktiiviset työntekijät 

        [HttpGet]
        [Route("")]
        public string[] GetAll()
        {

            string[] employeeNames = null;
            tuntidbContext context = new tuntidbContext();

           
                employeeNames = (from e in context.Employees
                                 where (e.Active == true)
                                 select e.FirstName + " " +
                                 e.LastName).ToArray();

                return employeeNames;
         }

        // Muut metodit...
    }

}
