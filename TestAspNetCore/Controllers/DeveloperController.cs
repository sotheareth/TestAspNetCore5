using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAspNetCore.Model;

namespace TestAspNetCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeveloperController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(Developer developer)
        {

            return Ok();
        }
    }
}
