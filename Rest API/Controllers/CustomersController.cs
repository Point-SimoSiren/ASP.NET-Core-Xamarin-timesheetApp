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
        public List<Customers> GetAll()
        {
            tuntidbContext context = new tuntidbContext();

            List<Customers> asiakkaat = context.Customers.ToList();

            return asiakkaat;
        }


    }
}