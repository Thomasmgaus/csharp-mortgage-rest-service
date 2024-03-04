using mortgage_application.Dto;

namespace mortgage_application.Model
{
    public class Schedule
    {
        public long PrincipleAmount { get; set; }
        public double AnnnualRate { get; set; }
        public int LoanMonths { get; set; }
        public String? StartDate { get; set; }
        public List<MonthlyPayment> Payments { get; set;}

        public Schedule(ApplicantDto applicantDto, List<MonthlyPayment> monthlyPayments) {
            PrincipleAmount = applicantDto.PrincipleAmount;
            AnnnualRate = applicantDto.AnnualRate;
            LoanMonths = applicantDto.LoanMonths;
            StartDate = applicantDto.StartDate.ToString();

            Payments = monthlyPayments;
        }

    }
}
