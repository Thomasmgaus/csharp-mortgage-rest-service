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
                return BadRequest($"Could not find user with {id}");
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
                Console.WriteLine($"Error creating applicant ", ex.ToString());  
                return BadRequest("Error creating applicant");
            }
        }

        
        [HttpPatch("update/{id:length(24)}")]
        public async Task<IActionResult> Patch(String id, ApplicantDto applicantDto)
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

                return Ok(JsonConvert.SerializeObject(applicant));

            } catch(Exception ex)
            {
                Console.WriteLine($"Error updating applicant with id {id} ", ex.ToString());  
                return BadRequest($"Error updating applicant with id {id}");
            }
        }
    }
}
