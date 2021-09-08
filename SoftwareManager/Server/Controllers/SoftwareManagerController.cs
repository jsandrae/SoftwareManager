using Microsoft.AspNetCore.Mvc;
using SoftwareManager.Server.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareManager.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class SoftwareManagerController : ControllerBase
    {
        private readonly ISoftwareManagerRepo _softwareManagerRepo;

        public SoftwareManagerController(ISoftwareManagerRepo softwareManagerRepo)
        {
            _softwareManagerRepo = softwareManagerRepo;
        }

        [HttpGet]
        [Route("GetAllSoftware")]
        public IActionResult GetAllSoftware()
        {

            return Ok(_softwareManagerRepo.GetAllSoftware());
        }

        [HttpGet]
        [Route("GetGreaterSoftware/{version}")]
        public IActionResult GetGreaterSoftware(string version)
        {
            return Ok(_softwareManagerRepo.GetGreaterSoftware(version));
        }
    }
}
