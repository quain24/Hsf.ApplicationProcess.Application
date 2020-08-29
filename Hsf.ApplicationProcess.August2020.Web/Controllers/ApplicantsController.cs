using Hsf.ApplicationProcess.August2020.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Hsf.ApplicationProcess.August2020.Data;
using Hsf.ApplicationProcess.August2020.Data.Repositories;

namespace Hsf.ApplicationProcess.August2020.Web.Controllers
{
    [ApiController]
    [Route("api/Applicants")] // route should be independent from class naming
    [Produces("application/json")]
    public class ApplicantsController : ControllerBase
    {
        private readonly IApplicantRepository _repository;
        private readonly ILogger<ApplicantsController> _logger;

        public ApplicantsController(IApplicantRepository repository, ILogger<ApplicantsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Applicant>> GetApplicantById(int id)
        {
            var applicant = await _repository.GetApplicantById(id);
            if (applicant is null)
                return NoContent();
            return Ok(applicant);
        }

        [HttpPost]
        public async Task<IActionResult> PostApplicant([FromBody] JsonElement body)
        {
            var json = body.GetRawText();
            var applicant = JsonSerializer.Deserialize<Applicant>(json);
            if(_repository.AddNewApplicant(applicant)) 
               return CreatedAtAction("GetApplicantById", new {id = applicant.ID}, applicant);
            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Applicant> DeleteApplicant(int id)
        {
            if (_repository.DeleteApplicant(id))
                return Ok();
            return NotFound();
        }
    }
}