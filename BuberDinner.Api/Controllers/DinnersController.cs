using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuberDinner.Api.Controllers
{
    [Route("[controller]")]
    public class DinnersController : ApiController
    {
        private readonly ILogger<DinnersController> _logger;

        public DinnersController(ILogger<DinnersController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult ListDinners()
        {
            return Ok(Array.Empty<string>());
        }
    }
}