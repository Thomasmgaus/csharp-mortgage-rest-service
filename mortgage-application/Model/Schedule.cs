using mortgage_application.Dto;

namespace mortgage_application.Model
{
    public class Schedule
    {
        public decimal PrincipleAmount { get; set; }
        public decimal AnnualRate { get; set; }
        public int LoanYears{ get; set; }
        public String? StartDate { get; set; }
        public List<MonthlyPayment> Payments { get; set;}

        public Schedule(ApplicantDto applicantDto, List<MonthlyPayment> monthlyPayments) {
            PrincipleAmount = applicantDto.PrincipleAmount;
            AnnualRate = applicantDto.AnnualRate;
            LoanYears = applicantDto.LoanYears;
            StartDate = applicantDto.StartDate.ToString();

            Payments = monthlyPayments;
        }

    }
}
