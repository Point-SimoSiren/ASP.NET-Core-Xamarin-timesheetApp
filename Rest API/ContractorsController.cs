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
    public class ContractorsController : ControllerBase
    {

        [HttpGet]
        [Route("")]
        public List<Contractors> GetAll()
        {
            tuntidbContext context = new tuntidbContext();

            List<Contractors> contractors = context.Contractors.ToList();

            return contractors;

        }
    }
}