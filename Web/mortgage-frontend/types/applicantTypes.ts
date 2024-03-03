export type Applicant = {
    name: string,
    principleAmount: number,
    annualRate: number,
    loanMonths: number,
    startDate: Date,
}

export type MonthlyPayment = {
    paymentDate: string,
    interestPaid: number,
    principlePaid: number,
    monthlyPayment: number,
    totalInterest: number,
    totalPayment: number,
}

export type MortgagePaymentByMonth = {
    id: string,
    monthlyMortgagePayment: MonthlyPayment[]
}