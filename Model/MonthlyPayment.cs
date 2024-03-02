namespace mortgage_application.Model
{
    public class MonthlyPayment
    {
        public DateTime PaymentDate;
        public double InterestPaid;
        public double PrinciplePaid;
        public double MontlthyPayment;
        public double TotalInterest;
        public double TotalPayment;

        public MonthlyPayment(DateTime paymentDate, double interestPaid, double principlePaid, double monthlyPayment, double totalInterest, double totalPayment)
        {
            this.PaymentDate = paymentDate;
            this.InterestPaid = interestPaid;
            this.PrinciplePaid = principlePaid;
            this.MontlthyPayment = monthlyPayment;
            this.TotalInterest = totalInterest;
            this.TotalPayment = totalPayment;
        }
    }
}
