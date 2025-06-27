using System;

namespace Models
{
    public abstract class Loan
    {
        public int LoanId { get; set; }
        public Customer Customer { get; set; }
        public double PrincipalAmount { get; set; }
        public double InterestRate { get; set; }
        public int LoanTerm { get; set; }
        public string LoanType { get; set; }
        public string LoanStatus { get; set; }

        public Loan() { }

        public Loan(int loanId, Customer customer, double principalAmount, double interestRate, int loanTerm, string loanType)
        {
            LoanId = loanId;
            Customer = customer;
            PrincipalAmount = principalAmount;
            InterestRate = interestRate;
            LoanTerm = loanTerm;
            LoanType = loanType;
            LoanStatus = "Pending";
        }

        public override string ToString()
        {
            return $"LoanId: {LoanId}, Type: {LoanType}, Status: {LoanStatus}, Principal: {PrincipalAmount}, InterestRate: {InterestRate}, Term: {LoanTerm} months";
        }
    }
}
