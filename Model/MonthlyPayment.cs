namespace mortgage_application.Model
{
    public class MonthlyPayment
    {
        public string PaymentDate;
        public double InterestPaid;
        public double PrinciplePaid;
        public double Payment;
        public double TotalInterest;
        public double TotalPayment;
        public double RemainingBalance;

        public MonthlyPayment(string paymentDate, double interestPaid, double principlePaid, double Payment, double remainingBalance, double totalInterest, double totalPayment)
        {
            this.PaymentDate = paymentDate;
            this.InterestPaid = interestPaid;
            this.PrinciplePaid = principlePaid;
            this.Payment = Payment;
            this.TotalInterest = totalInterest;
            this.TotalPayment = totalPayment;
            this.RemainingBalance = remainingBalance;
        }
    }
}
