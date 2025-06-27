using System;

namespace Models
{
    public class CarLoan : Loan
    {
        public string CarModel { get; set; }
        public double CarValue { get; set; }

        public CarLoan() { }

        public CarLoan(int loanId, Customer customer, double principal, double rate, int term, string carModel, double carValue)
            : base(loanId, customer, principal, rate, term, "CarLoan")
        {
            CarModel = carModel;
            CarValue = carValue;
        }

        public override string ToString()
        {
            return base.ToString() + $", Car Model: {CarModel}, Car Value: {CarValue}";
        }
    }
}
