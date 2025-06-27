using System;

namespace Models
{
    public class HomeLoan : Loan
    {
        public string PropertyAddress { get; set; }
        public double PropertyValue { get; set; }

        public HomeLoan() { }

        public HomeLoan(int loanId, Customer customer, double principal, double rate, int term, string address, double value)
            : base(loanId, customer, principal, rate, term, "HomeLoan")
        {
            PropertyAddress = address;
            PropertyValue = value;
        }

        public override string ToString()
        {
            return base.ToString() + $", Property Address: {PropertyAddress}, Property Value: {PropertyValue}";
        }
    }
}
