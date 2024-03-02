using Microsoft.AspNetCore.Mvc;
using mortgage_application.Model;
using mortgage_application.Services;
using mortgage_application.Dto;
using System.Text.Json;
using Newtonsoft.Json;

namespace mortgage_application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicantController : ControllerBase
    {
        public readonly ApplicantService _applicantService;

        public ApplicantController(ApplicantService applicantService) =>
            _applicantService = applicantService;


        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<String>> Get(string id)
        {
            var applicant = await _applicantService.GetAsync(id);

            if (applicant is null)
            {
                return NotFound();
            }

            return Ok(JsonConvert.SerializeObject(applicant));
        }


        [HttpPost]
        public async Task<IActionResult> Post(ApplicantDto createApplicantObject)
        {
            MortgageApplicationService ms = new MortgageApplicationService(createApplicantObject);

            Applicant newApplicant = ms.CreateApplicant();

            await _applicantService.CreateAsync(newApplicant);

            return CreatedAtAction(nameof(Post), new { id = newApplicant.Id }, JsonConvert.SerializeObject(newApplicant));
        }

    }
}
