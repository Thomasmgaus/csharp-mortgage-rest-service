using mortgage_application.Services;
using mortgage_application.Model;
using mortgage_application.Dto;

namespace mortgage_application_tests;

[TestFixture]
public class MortgageServiceTests 
{
    public Applicant _applicant;
    [SetUp]
    public void SetUp()
    {
        List<MonthlyPayment> mp = new List<MonthlyPayment>();

        MonthlyPayment one = new MonthlyPayment("3/19/2024", 29.17, 820.05, 849.22, 9179.95, 29.17, 849.22);
        MonthlyPayment two = new MonthlyPayment("4/19/2024", 55.94, 822.45, 1698.44, 8357.50, 26.77, 849.22);
        MonthlyPayment three = new MonthlyPayment("5/19/2024", 80.32, 824.84, 2547.66, 7532.66, 24.38, 849.22);

        mp.Add(one);
        mp.Add(two);
        mp.Add(three);

        ApplicantDto applicantDto = new ApplicantDto();
        applicantDto.PrincipleAmount = 10000;
        applicantDto.AnnualRate = 3.5;
        applicantDto.LoanYears = 1;
        applicantDto.StartDate = DateTime.Now;

        Schedule schedule = new Schedule(applicantDto, mp);

        _applicant = new Applicant();
        _applicant.AddSchedule(schedule);
    }

    [Test]
    public void TestCreateNonInitalizedPayload()
    {
        ApplicantDto applicantDto = new ApplicantDto();
        applicantDto.PrincipleAmount = 0;
        applicantDto.AnnualRate = 0;
        applicantDto.LoanYears = 0;

        MortgageApplicationService ms = new MortgageApplicationService(applicantDto);

        Applicant applicant = ms.CreateMortgageSchedule();

        Assert.IsTrue(applicant == null);

    }

    [Test]
    public void TestCreateNegativePayload()
    {
        ApplicantDto applicantDto = new ApplicantDto();
        applicantDto.PrincipleAmount = -160000;
        applicantDto.AnnualRate = -3.5;
        applicantDto.LoanYears = -15;
        applicantDto.StartDate = DateTime.Now;

        MortgageApplicationService ms = new MortgageApplicationService(applicantDto);

        Applicant applicant = ms.CreateMortgageSchedule();

        Assert.IsTrue(applicant == null);
    }

    [Test]
    public void TestCreatePracticalPayload()
    {
        ApplicantDto applicantDto = new ApplicantDto();
        applicantDto.PrincipleAmount = 160000;
        applicantDto.AnnualRate = 3.5;
        applicantDto.LoanYears = 15;
        applicantDto.StartDate =  DateTime.Now;

        MortgageApplicationService ms = new MortgageApplicationService(applicantDto);

        Applicant applicant = ms.CreateMortgageSchedule();

        Assert.IsTrue(applicant.Schedules.Count == 1);
        Assert.IsTrue(applicant.Schedules[0].Payments.Count == 15 * 12);
    }

    [Test]
    public void TestUpdateNonInitalizedPayload()
    {
        ApplicantDto applicantDto = new ApplicantDto();
        applicantDto.PrincipleAmount = 0;
        applicantDto.AnnualRate = 0;
        applicantDto.LoanYears = 0;

        MortgageApplicationService ms = new MortgageApplicationService(applicantDto);

        Applicant updatedApplicant = ms.AddMortgageSchedule(_applicant);

        Assert.IsTrue(updatedApplicant.Schedules.Count == 1);
    }

    [Test]
    public void TestUpdateNegativePayload()
    {
        ApplicantDto applicantDto = new ApplicantDto();
        applicantDto.PrincipleAmount = -160000;
        applicantDto.AnnualRate = -3.5;
        applicantDto.LoanYears = -15;
        applicantDto.StartDate = DateTime.Now;

        MortgageApplicationService ms = new MortgageApplicationService(applicantDto);

        Applicant updatedApplicant = ms.AddMortgageSchedule(_applicant);

        Assert.IsTrue(updatedApplicant.Schedules.Count == 1);
    }

    [Test]
    public void TestUpdatePracticalPayload()
    {
        ApplicantDto applicantDto = new ApplicantDto();
        applicantDto.PrincipleAmount = 160000;
        applicantDto.AnnualRate = 3.5;
        applicantDto.LoanYears = 15;
        applicantDto.StartDate =  DateTime.Now;

        MortgageApplicationService ms = new MortgageApplicationService(applicantDto);

        Applicant updatedApplicant = ms.AddMortgageSchedule(_applicant);

        Assert.IsTrue(updatedApplicant.Schedules.Count == 2);
        Assert.IsTrue(updatedApplicant.Schedules[1].Payments.Count == 15 * 12);
    }
}