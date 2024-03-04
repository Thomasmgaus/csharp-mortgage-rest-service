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

export type Schedule = {
    PrincipleAmount: number,
    AnnualRate: number,
    LoanMonths: number,
    StartDate: Date,
    Payments: MonthlyPayment[]
}

export type MortgagePaymentByMonth = {
    Id: string,
    Schedules: Schedule[]
}