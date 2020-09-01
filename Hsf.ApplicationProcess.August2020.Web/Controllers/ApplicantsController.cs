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
                var errMsg = _localizer["crud_errors.get_failed", new{id}].Value;
                _logger.LogInformation(errMsg);
                return NotFound(errMsg);
            }

            var okMsg = _localizer["crud_ok.get_returned", new {id}].Value;
            _logger.LogInformation(okMsg);
            return Ok(okMsg);
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
                _logger.LogInformation(_localizer["crud_ok.update_updated", new{id = applicant.ID}]);
                return CreatedAtAction(nameof(GetApplicantById), new { id = applicant.ID }, applicant);
            }

            var msg = _localizer["crud_errors.update_failed_no_id", new {id}].Value;
            _logger.LogInformation(msg);
            return NotFound(msg);
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
                _logger.LogInformation(_localizer["crud_ok.add_success", new {id = applicant.ID}].Value);
                return CreatedAtAction(nameof(GetApplicantById), new { id = applicant.ID }, applicant);
            }

            var msg = _localizer["crud_errors.add_failed_db_error"].Value;
            _logger.LogError(msg);
            return BadRequest(msg);
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
                var msg = _localizer["crud_errors.common_id_negative"].Value;
                _logger.LogError(msg);
                return BadRequest(msg);
            }

            if (await _repository.DeleteApplicantAsync(id))
            {
                var msgOk = _localizer["crud_ok.delete_success", new {id}].Value;
                _logger.LogInformation(msgOk);
                return Ok(msgOk);
            }

            var msgError = _localizer["crud_errors.delete_failed_no_id", new {id}].Value;
            _logger.LogInformation(msgError);
            return NotFound(msgError);
        }
    }
}