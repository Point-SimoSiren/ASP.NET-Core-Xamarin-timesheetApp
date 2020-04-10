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
    public class CustomersController : ControllerBase
    {


        [HttpGet]
        [Route("")]
        public string[] GetAll()
        {

            string[] customerNames = null;
            tuntidbContext context = new tuntidbContext();


            customerNames = (from c in context.Customers
                             where (c.Active == true)
                             select c.CustomerName).ToArray();

            return customerNames;
        }

        // Muut metodit...
    }

}
