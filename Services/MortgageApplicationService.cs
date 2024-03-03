using mortgage_application.Dto;
using mortgage_application.Model;
using System.Collections.ObjectModel;

namespace mortgage_application.Services
{
    public class MortgageApplicationService
    {
        private ApplicantDto ApplicantDto;

        public MortgageApplicationService(ApplicantDto applicantDto)
        {
           this.ApplicantDto = applicantDto;
        }

        public Applicant CreateApplicant()
        {
            return new Applicant(GenerateMonthlyMortgageRates());
        }

        public List<MonthlyPayment> GenerateMonthlyMortgageRates()
        {
            List<MonthlyPayment> generateMortagePayments = new List<MonthlyPayment>();

            var monlthyInterestRate = (this.ApplicantDto.AnnualRate / 12);

            double remainder = this.ApplicantDto.PrincipleAmount;

            var exponent = Math.Pow((1 + monlthyInterestRate), this.ApplicantDto.LoanMonths);
            var numerator = (monlthyInterestRate * exponent);
            var denomenator = exponent - 1;

            var monthlyPayment = Math.Round((remainder * (numerator / denomenator)), 2);

            double totalInterest = 0;

            for (int i = 0; i < this.ApplicantDto.LoanMonths; i++)
            {
                var interest = Math.Round((monlthyInterestRate * remainder), 2);
                totalInterest = Math.Round(totalInterest + interest, 2);
                var thisMonthPrinciple = Math.Round((monthlyPayment - interest), 2);
                remainder = Math.Round((remainder - thisMonthPrinciple), 2);
                
                MonthlyPayment record = new MonthlyPayment(
                    ApplicantDto.StartDate.AddMonths(i).ToShortDateString(),
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
