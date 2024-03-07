namespace mortgage_application.Model
{
    public class MonthlyPayment
    {
        public string PaymentDate;
        public string InterestPaid;
        public string PrinciplePaid;
        public string Payment;
        public string TotalInterest;
        public string TotalPayment;
        public string RemainingBalance;

        public MonthlyPayment(string paymentDate,  string interestPaid, string principlePaid, string Payment, string remainingBalance, string totalInterest, string totalPayment)
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
