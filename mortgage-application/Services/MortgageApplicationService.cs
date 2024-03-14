using Microsoft.AspNetCore.Mvc.Infrastructure;
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
            return applicant;
       }

        private Boolean VerifyDto()
        {
            if (ApplicantDto.PrincipleAmount < 1 ||
                ApplicantDto.AnnualRate < 1 || 
                ApplicantDto.LoanYears < 1 || 
                ApplicantDto.StartDate == null)
            {
                return false;
            }
            return true;
        }

        private string Format(decimal number)
        {
            return number.ToString("C2");
        }

        private List<MonthlyPayment> GenerateMonthlyMortgageRates()
        {
            List<MonthlyPayment> generateMortagePayments = new List<MonthlyPayment>();

            var loanMonths = ApplicantDto.LoanYears * 12;
            decimal remainder = ApplicantDto.PrincipleAmount;
            DateTime startDate = ApplicantDto.StartDate ?? DateTime.Now;
            var monlthyInterestRate = (ApplicantDto.AnnualRate / 100) / 12;


            var exponent = Convert.ToDecimal(Math.Pow(1 + Convert.ToDouble(monlthyInterestRate), loanMonths));
            var numerator = monlthyInterestRate * exponent;
            var denomenator = exponent - 1;

            var monthlyPayment = remainder * (numerator / denomenator);

            decimal totalInterest = 0;

            for (int i = 0; i < loanMonths; i++)
            {
                var interest = monlthyInterestRate * remainder;
                totalInterest = totalInterest + interest;
                var thisMonthPrinciple = monthlyPayment - interest;
                remainder = remainder - thisMonthPrinciple;

                if(remainder < 0) remainder = 0;
                
                
                MonthlyPayment record = new MonthlyPayment(
                    startDate.AddMonths(i).ToShortDateString(),
                    Format(interest), 
                    Format(thisMonthPrinciple), 
                    Format(monthlyPayment),
                    Format(remainder),
                    Format(totalInterest), 
                    Format(monthlyPayment * (i + 1))
                   );
                generateMortagePayments.Add(record);
            }

            return generateMortagePayments;
        }
    }
}
