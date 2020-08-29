using Hsf.ApplicationProcess.August2020.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Hsf.ApplicationProcess.August2020.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ApplicantsController : ControllerBase
    {
        private readonly ILogger<ApplicantsController> _logger;

        public ApplicantsController(ILogger<ApplicantsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Applicant> Get()
        {
            return new Applicant[]
            {
                new Applicant()
                {
                    Address = "Ułańska 3/2",
                    Age = 32,
                    CountryOfOrigin = "Poland",
                    EmailAddress = "hw@wp.pl",
                    FamilyName = "Wiśniewski",
                    Hired = true,
                    Name = "Henryk"
                }
            };
        }
    }
}