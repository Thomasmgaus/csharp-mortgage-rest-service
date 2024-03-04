export type Applicant = {
    principleAmount: number,
    annualRate: number,
    loanMonths: number,
    startDate: Date,
}

export type MonthlyPayment = {
    PaymentDate: string,
    InterestPaid: number,
    PrinciplePaid: number,
    Payment: number,
    TotalInterest: number,
    TotalPayment: number,
    RemainingBalance: number,
}

export type MortgagePaymentByMonth = {
    Id: string,
    MonthlyMortgagePayment: MonthlyPayment[]
}