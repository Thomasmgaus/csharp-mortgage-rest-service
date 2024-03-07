using Microsoft.AspNetCore.Mvc;
using mortgage_application.Model;
using mortgage_application.Services;
using mortgage_application.Dto;
using Newtonsoft.Json;
namespace mortgage_application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicantController : ControllerBase
    {
        public readonly ApplicantService _applicantService;

        public ApplicantController(ApplicantService applicantService) =>
            _applicantService = applicantService;


        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<String>> Get(string id)
        {
            try
            {
                var applicant = await _applicantService.GetAsync(id);

                if (applicant is null)
                {
                    return NotFound();
                }

                 return Ok(JsonConvert.SerializeObject(applicant));
            } catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            } 
        }

        [HttpPost]
        public async Task<IActionResult> Post(ApplicantDto createApplicantObject)
        {
            try
            {
                MortgageApplicationService ms = new MortgageApplicationService(createApplicantObject);

                Applicant newApplicant = ms.CreateMortgageSchedule();

                if(newApplicant == null)
                {
                    return BadRequest();
                }

                await _applicantService.CreateAsync(newApplicant);

                return CreatedAtAction(nameof(Post), new { id = newApplicant.Id }, JsonConvert.SerializeObject(newApplicant));

            } catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        
        [HttpPut("update/{id:length(24)}")]
        public async Task<IActionResult> Put(String id, ApplicantDto applicantDto)
        {
            try
            {
                var applicant = await _applicantService.GetAsync(id);

                if(applicant is null)
                {
                    return NotFound();
                }
                
                MortgageApplicationService ms = new MortgageApplicationService(applicantDto);

                Applicant updatedApplicant = ms.AddMortgageSchedule(applicant);

                await _applicantService.UpdateAsync(id, updatedApplicant);

                return CreatedAtAction(nameof(Post), new { id = updatedApplicant.Id }, JsonConvert.SerializeObject(updatedApplicant));

            } catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
