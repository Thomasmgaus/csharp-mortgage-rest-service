using mortgage_application.Dto;
using mortgage_application.Model;

namespace mortgage_application.Services
{
    public class MortgageApplicationService
    {
        private ApplicantDto ApplicantDto;

        public MortgageApplicationService(ApplicantDto applicantDto)
        {
           ApplicantDto = applicantDto;
        }

        public Applicant CreateMortgageSchedule()
        {   
            if(VerifyDto())
            {
                Applicant applicant = new Applicant();
                Schedule schedule = new Schedule(ApplicantDto, GenerateMonthlyMortgageRates());

                applicant.AddSchedule(schedule);

                return applicant;
            }
            return null;
        }

        public Applicant AddMortgageSchedule(Applicant applicant)
        {
            if(VerifyDto())
            {
                Schedule schedule = new Schedule(ApplicantDto, GenerateMonthlyMortgageRates());

                applicant.AddSchedule(schedule);

                return applicant;
            }
            return null;
       }

        private Boolean VerifyDto()
        {
            if (ApplicantDto.PrincipleAmount == null || ApplicantDto.PrincipleAmount < 1 ||
                ApplicantDto.AnnualRate == null || ApplicantDto.AnnualRate < 1 || 
                ApplicantDto.LoanYears == null || ApplicantDto.LoanYears < 1 || 
                ApplicantDto.StartDate == null)
            {
                return false;
            }
            return true;
        }

        private List<MonthlyPayment> GenerateMonthlyMortgageRates()
        {
            List<MonthlyPayment> generateMortagePayments = new List<MonthlyPayment>();

            var loanMonths = ApplicantDto.LoanYears * 12;
            double remainder = ApplicantDto.PrincipleAmount;
            DateTime startDate = ApplicantDto.StartDate ?? DateTime.Now;
            var monlthyInterestRate = (ApplicantDto.AnnualRate / 100) / 12;


            var exponent = Math.Pow(1 + monlthyInterestRate, loanMonths);
            var numerator = monlthyInterestRate * exponent;
            var denomenator = exponent - 1;

            var monthlyPayment = Math.Round(remainder * (numerator / denomenator), 2);

            double totalInterest = 0;

            for (int i = 0; i < loanMonths; i++)
            {
                var interest = Math.Round(monlthyInterestRate * remainder, 2);
                totalInterest = Math.Round(totalInterest + interest, 2);
                var thisMonthPrinciple = Math.Round(monthlyPayment - interest, 2);
                remainder = Math.Round(remainder - thisMonthPrinciple, 2);

                if(remainder < 0) remainder = 0;
                
                
                MonthlyPayment record = new MonthlyPayment(
                    startDate.AddMonths(i).ToShortDateString(),
                    interest, 
                    thisMonthPrinciple, 
                    monthlyPayment,
                    remainder,
                    totalInterest, 
                    Math.Round(monthlyPayment * (i + 1), 2)
                   );
                generateMortagePayments.Add(record);
            }

            return generateMortagePayments;
        }
    }
}
