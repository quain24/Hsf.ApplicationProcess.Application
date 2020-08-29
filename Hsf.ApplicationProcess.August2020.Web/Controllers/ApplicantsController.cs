using Hsf.ApplicationProcess.August2020.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Hsf.ApplicationProcess.August2020.Data;
using Hsf.ApplicationProcess.August2020.Data.Repositories;
using Hsf.ApplicationProcess.August2020.Web.Extensions;

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
            var applicant = await _repository.GetApplicantByIdAsync(id);
            if (applicant is null)
                return NoContent();
            return Ok(applicant);
        }

        [HttpPut]
        public async Task<ActionResult<Applicant>> UpdateApplicant([FromBody] JsonElement applicantJson)
        {

            var applicant = applicantJson.Deserialize<Applicant>();

            if(await _repository.UpdateApplicantAsync(0, applicant) >= 1)
                return CreatedAtAction(nameof(GetApplicantById), new { id = applicant.ID }, applicant);
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<Applicant>> PostApplicant([FromBody] JsonElement applicantJson)
        {
            var applicant = applicantJson.Deserialize<Applicant>();
            if(await _repository.AddNewApplicantAsync(applicant) > 0) 
               return CreatedAtAction(nameof(GetApplicantById), new {id = applicant.ID}, applicant);
            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Applicant>> DeleteApplicant(int id)
        {
            if (await _repository.DeleteApplicantAsync(id)>=1)
                return Ok();
            return NotFound();
        }
    }
}