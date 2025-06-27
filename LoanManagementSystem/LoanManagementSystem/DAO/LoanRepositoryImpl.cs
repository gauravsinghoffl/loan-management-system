using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Exceptions;
using Models;
using Data;

namespace DAO
{
    public class LoanRepositoryImpl : ILoanRepository
    {
        private readonly SqlConnection conn;

        public LoanRepositoryImpl()
        {
            conn = DBConnUtil.GetDBConnection("LoanDB");
        }

        public void ApplyLoan(Loan loan)
        {
            Console.Write("Confirm apply loan? (Yes/No): ");
            var confirm = Console.ReadLine();
            if (confirm?.ToLower() != "yes") return;

            conn.Open();

            // Insert customer if not exists
            SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Customer WHERE CustomerId = @CustomerId", conn);
            checkCmd.Parameters.AddWithValue("@CustomerId", loan.Customer.CustomerId);
            int count = (int)checkCmd.ExecuteScalar();

            if (count == 0)
            {
                SqlCommand insertCustomerCmd = new SqlCommand(
                    "INSERT INTO Customer (CustomerId, Name, EmailAddress, PhoneNumber, Address, CreditScore) " +
                    "VALUES (@CustomerId, @Name, @Email, @Phone, @Address, @CreditScore)", conn);

                insertCustomerCmd.Parameters.AddWithValue("@CustomerId", loan.Customer.CustomerId);
                insertCustomerCmd.Parameters.AddWithValue("@Name", loan.Customer.Name);
                insertCustomerCmd.Parameters.AddWithValue("@Email", loan.Customer.EmailAddress);
                insertCustomerCmd.Parameters.AddWithValue("@Phone", loan.Customer.PhoneNumber);
                insertCustomerCmd.Parameters.AddWithValue("@Address", loan.Customer.Address);
                insertCustomerCmd.Parameters.AddWithValue("@CreditScore", loan.Customer.CreditScore);
                insertCustomerCmd.ExecuteNonQuery();
            }

            // Insert Loan
            SqlCommand cmd = new SqlCommand(
                "INSERT INTO Loan (LoanId, CustomerId, PrincipalAmount, InterestRate, LoanTerm, LoanType, LoanStatus) " +
                "VALUES (@LoanId, @CustomerId, @PrincipalAmount, @InterestRate, @LoanTerm, @LoanType, @LoanStatus)", conn);

            cmd.Parameters.AddWithValue("@LoanId", loan.LoanId);
            cmd.Parameters.AddWithValue("@CustomerId", loan.Customer.CustomerId);
            cmd.Parameters.AddWithValue("@PrincipalAmount", loan.PrincipalAmount);
            cmd.Parameters.AddWithValue("@InterestRate", loan.InterestRate);
            cmd.Parameters.AddWithValue("@LoanTerm", loan.LoanTerm);
            cmd.Parameters.AddWithValue("@LoanType", loan.LoanType);
            cmd.Parameters.AddWithValue("@LoanStatus", "Pending");

            cmd.ExecuteNonQuery();

            conn.Close();
            Console.WriteLine("Loan applied successfully.");
        }


        public double CalculateInterest(int loanId)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT PrincipalAmount, InterestRate, LoanTerm FROM Loan WHERE LoanId = @LoanId", conn);
            cmd.Parameters.AddWithValue("@LoanId", loanId);

            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                conn.Close();
                throw new InvalidLoanException("Loan not found.");
            }

            double principal = Convert.ToDouble(reader["PrincipalAmount"]);
            double rate = Convert.ToDouble(reader["InterestRate"]);
            int term = Convert.ToInt32(reader["LoanTerm"]);
            conn.Close();

            return CalculateInterest(principal, rate, term);
        }

        public double CalculateInterest(double principal, double rate, int term)
        {
            return (principal * rate * term) / 1200;
        }

        public void LoanStatus(int loanId)
        {
            conn.Open();

            SqlCommand getScoreCmd = new SqlCommand("SELECT c.CreditScore FROM Loan l JOIN Customer c ON l.CustomerId = c.CustomerId WHERE l.LoanId = @LoanId", conn);
            getScoreCmd.Parameters.AddWithValue("@LoanId", loanId);
            object result = getScoreCmd.ExecuteScalar();

            if (result == null)
            {
                conn.Close();
                throw new InvalidLoanException("Loan not found.");
            }

            int creditScore = Convert.ToInt32(result);
            string status = creditScore > 650 ? "Approved" : "Rejected";

            SqlCommand updateCmd = new SqlCommand("UPDATE Loan SET LoanStatus = @Status WHERE LoanId = @LoanId", conn);
            updateCmd.Parameters.AddWithValue("@Status", status);
            updateCmd.Parameters.AddWithValue("@LoanId", loanId);
            updateCmd.ExecuteNonQuery();

            conn.Close();
            Console.WriteLine($"Loan status updated to: {status}");
        }

        public double CalculateEMI(int loanId)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT PrincipalAmount, InterestRate, LoanTerm FROM Loan WHERE LoanId = @LoanId", conn);
            cmd.Parameters.AddWithValue("@LoanId", loanId);

            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                conn.Close();
                throw new InvalidLoanException("Loan not found.");
            }

            double p = Convert.ToDouble(reader["PrincipalAmount"]);
            double r = Convert.ToDouble(reader["InterestRate"]) / 1200;
            int n = Convert.ToInt32(reader["LoanTerm"]);

            conn.Close();
            return CalculateEMI(p, r, n);
        }

        public double CalculateEMI(double p, double r, int n)
        {
            double emi = (p * r * Math.Pow(1 + r, n)) / (Math.Pow(1 + r, n) - 1);
            return Math.Round(emi, 2);
        }

        public void LoanRepayment(int loanId, double amount)
        {
            double emi = CalculateEMI(loanId);
            int emiCount = (int)(amount / emi);

            if (emiCount < 1)
            {
                Console.WriteLine("Amount too low to cover one EMI. Payment rejected.");
                return;
            }

            Console.WriteLine($"Payment accepted. You can pay {emiCount} EMI(s) of amount {emi} each.");
        }

        public List<Loan> GetAllLoans()
        {
            List<Loan> loans = new List<Loan>();
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Loan", conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Loan loan = new CarLoan();
                loan.LoanId = Convert.ToInt32(reader["LoanId"]);
                loan.PrincipalAmount = Convert.ToDouble(reader["PrincipalAmount"]);
                loan.InterestRate = Convert.ToDouble(reader["InterestRate"]);
                loan.LoanTerm = Convert.ToInt32(reader["LoanTerm"]);
                loan.LoanType = reader["LoanType"].ToString();
                loan.LoanStatus = reader["LoanStatus"].ToString();
                loans.Add(loan);
            }

            conn.Close();
            return loans;
        }

        public Loan GetLoanById(int loanId)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Loan WHERE LoanId = @LoanId", conn);
            cmd.Parameters.AddWithValue("@LoanId", loanId);
            SqlDataReader reader = cmd.ExecuteReader();

            if (!reader.Read())
            {
                conn.Close();
                throw new InvalidLoanException("Loan not found.");
            }

            Loan loan = new CarLoan();
            loan.LoanId = Convert.ToInt32(reader["LoanId"]);
            loan.PrincipalAmount = Convert.ToDouble(reader["PrincipalAmount"]);
            loan.InterestRate = Convert.ToDouble(reader["InterestRate"]);
            loan.LoanTerm = Convert.ToInt32(reader["LoanTerm"]);
            loan.LoanType = reader["LoanType"].ToString();
            loan.LoanStatus = reader["LoanStatus"].ToString();

            conn.Close();
            return loan;
        }
    }
}
