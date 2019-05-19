using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JapLang.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JapLang.Controllers
{
    [Produces("application/json")]
    [Route("api/tests")]
    public class TestsController : Controller
    {
        private readonly ITestsService _testService;

        public TestsController(ITestsService serv)
        {
            _testService = serv;
        }

        [HttpGet("jap")]
        public async Task<IActionResult> GetTestJap()
        {
            var res = _testService.GetTestJap();
            return Ok(res);
        }
    }
}