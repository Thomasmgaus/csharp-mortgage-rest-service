using mortgage_application.Services;
using mortgage_application.Model;
using mortgage_application.Dto;

namespace mortgage_application_tests;

[TestFixture]
public class MortgageServiceTests 
{

    [Test]
    public void TestEmptyPayload()
    {
        ApplicantDto applicantDto = new ApplicantDto();
        applicantDto.PrincipleAmount = 0;
        applicantDto.AnnualRate = 0;
        applicantDto.LoanYears = 0;
        applicantDto.StartDate = null;

        MortgageApplicationService ms = new MortgageApplicationService(applicantDto);

        Applicant applicant = ms.CreateMortgageSchedule();

        Console.WriteLine("Applicant created, ", applicant.GetType());

        Assert.IsTrue(applicant.Schedules.Count() == 1);

    }
}