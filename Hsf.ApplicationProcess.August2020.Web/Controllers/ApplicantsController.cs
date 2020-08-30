using Hsf.ApplicationProcess.August2020.Data.Repositories;
using Hsf.ApplicationProcess.August2020.Domain.Models;
using Hsf.ApplicationProcess.August2020.Web.Extensions;
using Hsf.ApplicationProcess.August2020.Web.SwaggerExamples;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

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

        /// <summary>
        /// Return Applicant by ID number
        /// </summary>
        /// <param name="id" example="1">Applicants unique ID number</param>
        [HttpGet("{id}", Name = "GetApplicantById")]
        [SwaggerResponse((int)HttpStatusCode.OK, "ID exists and corresponding Applicant has been returned.", typeof(Applicant))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "ID has not been found!", typeof(IDictionary<string, string>))]
        public async Task<ActionResult<Applicant>> GetApplicantById(int id)
        {
            var applicant = await _repository.GetApplicantByIdAsync(id);
            if (applicant is null)
                return NotFound();
            return Ok(applicant);
        }

        ///<summary>
        /// Updates applicant data with provided ones.
        /// </summary>
        /// <param name="applicantJson">And updated applicant data in json format</param>
        [HttpPut]
        [SwaggerRequestExample(typeof(Applicant), typeof(ApplicantUpdateExample))]
        [SwaggerResponse((int)HttpStatusCode.Created, "ID exists and was updated.", typeof(Applicant))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Provided applicant ID does not exist", typeof(IDictionary<string, string>))]
        public async Task<ActionResult<Applicant>> UpdateApplicant([FromHeader] int id, [FromBody] Applicant applicant)
        {
            applicant.ID = id;

            if (await _repository.UpdateApplicantAsync(id, applicant))
            {
                await _repository.SaveChangesAsync();
                return CreatedAtAction(nameof(GetApplicantById), new { id = applicant.ID }, applicant);
            }
            return NotFound();
        }

        ///<summary>
        /// Adds new applicant to database.
        /// </summary>
        /// <param name="applicantJson">New applicant data in json format</param>
        [HttpPost]
        [SwaggerRequestExample(typeof(Applicant), typeof(ApplicantExample))]
        [SwaggerResponse((int)HttpStatusCode.Created, "ID exists and was updated.", typeof(Applicant))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Provided applicant ID does not exist", typeof(IDictionary<string, string>))]
        public async Task<ActionResult<Applicant>> PostApplicant([FromBody] Applicant applicant)
        {
            if (await _repository.AddNewApplicantAsync(applicant))
            {
                _repository.SaveChangesAsync();
                return CreatedAtAction(nameof(GetApplicantById), new { id = applicant.ID }, applicant);
            }
            return NotFound();
        }

        /// <summary>
        /// Deletes Applicant with provided ID number
        /// </summary>
        /// <param name="id" example="1">Applicants unique ID number</param>
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "ID exists and was updated.", typeof(IDictionary<string, string>))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Provided applicant ID does not exist", typeof(IDictionary<string, string>))]
        public async Task<IActionResult> DeleteApplicant(int id)
        {
            if (await _repository.DeleteApplicantAsync(id))
                return Ok();
            return NotFound();
        }
    }
}