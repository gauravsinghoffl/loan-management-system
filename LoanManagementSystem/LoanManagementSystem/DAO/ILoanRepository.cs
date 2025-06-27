using System.Collections.Generic;
using Models;

namespace DAO
{
    public interface ILoanRepository
    {
        void ApplyLoan(Loan loan);
        double CalculateInterest(int loanId);
        double CalculateInterest(double principal, double rate, int term);
        void LoanStatus(int loanId);
        double CalculateEMI(int loanId);
        double CalculateEMI(double principal, double rate, int term);
        void LoanRepayment(int loanId, double amount);
        List<Loan> GetAllLoans();
        Loan GetLoanById(int loanId);
    }
}
