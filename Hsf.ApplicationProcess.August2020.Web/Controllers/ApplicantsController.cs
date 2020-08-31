using Hsf.ApplicationProcess.August2020.Data.Repositories;
using Hsf.ApplicationProcess.August2020.Domain.Models;
using Hsf.ApplicationProcess.August2020.Web.Extensions;
using Hsf.ApplicationProcess.August2020.Web.SwaggerExamples;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Hsf.ApplicationProcess.August2020.Web.DTO;
using Microsoft.Extensions.Localization;

namespace Hsf.ApplicationProcess.August2020.Web.Controllers
{
    [ApiController]
    [Route("api/Applicants")] // route should be independent from class naming
    [Produces("application/json")]
    public class ApplicantsController : ControllerBase
    {
        private readonly IApplicantRepository _repository;
        private readonly ILogger<ApplicantsController> _logger;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ApplicantsController> _localizer;

        public ApplicantsController(IApplicantRepository repository, ILogger<ApplicantsController> logger, IMapper mapper, IStringLocalizer<ApplicantsController> localizer)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _localizer = localizer;
        }

        /// <summary>
        /// Return Applicant by ID number
        /// </summary>
        /// <param name="id" example="1">Applicants unique ID number</param>
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "ID exists and corresponding Applicant has been returned.", typeof(Applicant))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "ID has not been found!", typeof(IDictionary<string, string>))]
        public async Task<ActionResult<Applicant>> GetApplicantById([Range(0, int.MaxValue)] int id)
        {
            var applicant = await _repository.GetApplicantByIdAsync(id);
            if (applicant is null)
            {
                var msg = _localizer["crud_errors.get_failed_msg", new {id}];
                _logger.LogInformation(msg);
                return NotFound(msg);
            }
            _logger.LogInformation($"Found and returned applicant with id {id}.");
            return Ok(applicant);
        }

        ///<summary>
        /// Updates applicant data with provided ones.
        /// </summary>
        /// <param name="id" example="1">Target ID</param>
        /// <param name="applicantUpdateDto">And updated applicant data in json format</param>
        [HttpPut]
        [SwaggerRequestExample(typeof(ApplicantNoIdDTO), typeof(ApplicantNoIdDTOExample))]
        [SwaggerResponse((int)HttpStatusCode.Created, "ID exists and was updated.", typeof(Applicant))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Provided applicant ID does not exist", typeof(string))]
        public async Task<ActionResult<Applicant>> UpdateApplicant([FromHeader][Range(0, int.MaxValue)] int id, [FromBody] ApplicantNoIdDTO applicantUpdateDto)
        {
            var applicant =_mapper.Map<Applicant>(applicantUpdateDto);
            applicant.ID = id;

            if (!ModelState.IsValid)
            {
                _logger.LogAllErrorsFrom(nameof(UpdateApplicant), ModelState);
                return BadRequest(ModelState);
            }

            if (await _repository.UpdateApplicantAsync(id, applicant))
            {
                await _repository.SaveChangesAsync();
                _logger.LogInformation($"Applicant updated: ID - {applicant.ID}");
                return CreatedAtAction(nameof(GetApplicantById), new { id = applicant.ID }, applicant);
            }
            _logger.LogInformation($"Update failed - there is no applicant with id {id}.");
            return NotFound($"Update failed - there is no applicant with id {id}.");
        }

        ///<summary>
        /// Adds new applicant to database.
        /// </summary>
        /// <param name="applicant">New applicant data in json format</param>
        [HttpPost]
        [SwaggerRequestExample(typeof(ApplicantNoIdDTO), typeof(ApplicantNoIdDTOExample))]
        [SwaggerResponse((int)HttpStatusCode.Created, "Applicant has been created.", typeof(Applicant))]
        public async Task<ActionResult<Applicant>> PostApplicant([FromBody] ApplicantNoIdDTO applicantPostDto)
        {
            var applicant = _mapper.Map<Applicant>(applicantPostDto);

            if (!ModelState.IsValid)
            {
                _logger.LogAllErrorsFrom(nameof(PostApplicant), ModelState);
                return BadRequest(ModelState);
            }

            if (await _repository.AddNewApplicantAsync(applicant))
            {
                await _repository.SaveChangesAsync();
                _logger.LogInformation($"New applicant added: ID - {applicant.ID}");
                return CreatedAtAction(nameof(GetApplicantById), new { id = applicant.ID }, applicant);
            }
            _logger.LogError("Could not add new applicant to database!");
            return BadRequest();
        }

        /// <summary>
        /// Deletes Applicant with provided ID number
        /// </summary>
        /// <param name="id" example="1">Applicants unique ID number</param>
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "ID exists and corresponding Applicant has been removed.")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Provided applicant ID does not exist", typeof(string))]
        public async Task<IActionResult> DeleteApplicant([Range(0, int.MaxValue)] int id)
        {
            if (id < 0)
            {
                _logger.LogError("ID cannot be negative.");
                return BadRequest("ID cannot be negative.");
            }

            if (await _repository.DeleteApplicantAsync(id))
            {
                _logger.LogInformation($"Applicant with id {id} has been deleted.");
                return Ok();
            }
            _logger.LogInformation($"Deletion failed - there is no applicant with id {id}.");
            return NotFound();
        }
    }
}