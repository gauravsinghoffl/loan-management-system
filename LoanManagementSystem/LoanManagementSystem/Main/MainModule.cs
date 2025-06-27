using System;
using Models;
using DAO;
using Data;
using Exceptions;

namespace MainApp
{
    class MainModule
    {
        static void Main()
        {
            ILoanRepository repo = new LoanRepositoryImpl();

            while (true)
            {
                Console.WriteLine("\n===== Loan Management Menu =====");
                Console.WriteLine("1. Apply Loan");
                Console.WriteLine("2. View All Loans");
                Console.WriteLine("3. Get Loan by ID");
                Console.WriteLine("4. Loan Status Update");
                Console.WriteLine("5. Calculate EMI");
                Console.WriteLine("6. Repay Loan");
                Console.WriteLine("7. Exit");
                Console.Write("Choose option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Write("Enter Customer ID: ");
                        int custId = int.Parse(Console.ReadLine());

                        Console.Write("Enter Name: ");
                        string name = Console.ReadLine();

                        Console.Write("Enter Email Address: ");
                        string email = Console.ReadLine();

                        Console.Write("Enter Phone Number: ");
                        string phone = Console.ReadLine();

                        Console.Write("Enter Address: ");
                        string address = Console.ReadLine();

                        Console.Write("Enter Credit Score: ");
                        int creditScore = int.Parse(Console.ReadLine());

                        Customer customer = new Customer(custId, name, email, phone, address, creditScore);

                        Console.Write("Enter Loan ID: ");
                        int newLoanId = int.Parse(Console.ReadLine());

                        Console.Write("Enter Principal Amount: ");
                        double principal = double.Parse(Console.ReadLine());

                        Console.Write("Enter Interest Rate: ");
                        double interestRate = double.Parse(Console.ReadLine());

                        Console.Write("Enter Loan Term (months): ");
                        int loanTerm = int.Parse(Console.ReadLine());

                        Console.Write("Enter Loan Type (CarLoan/HomeLoan): ");
                        string loanType = Console.ReadLine();

                        Loan loan;
                        if (loanType == "CarLoan")
                        {
                            Console.Write("Enter Car Model: ");
                            string carModel = Console.ReadLine();

                            Console.Write("Enter Car Value: ");
                            double carValue = double.Parse(Console.ReadLine());

                            loan = new CarLoan(newLoanId, customer, principal, interestRate, loanTerm, carModel, carValue);
                        }
                        else if (loanType == "HomeLoan")
                        {
                            Console.Write("Enter Property Address: ");
                            string propAddr = Console.ReadLine();

                            Console.Write("Enter Property Value: ");
                            double propValue = double.Parse(Console.ReadLine());

                            loan = new HomeLoan(newLoanId, customer, principal, interestRate, loanTerm, propAddr, propValue);
                        }
                        else
                        {
                            Console.WriteLine("Invalid loan type.");
                            break;
                        }

                        repo.ApplyLoan(loan);
                        break;


                    case "2":
                        foreach (var l in repo.GetAllLoans())
                            Console.WriteLine(l);
                        break;

                    case "3":
                        Console.Write("Enter Loan ID: ");
                        int lid = int.Parse(Console.ReadLine());
                        Console.WriteLine(repo.GetLoanById(lid));
                        break;

                    case "4":
                        Console.Write("Enter Loan ID to check status: ");
                        repo.LoanStatus(int.Parse(Console.ReadLine()));
                        break;

                    case "5":
                        Console.Write("Enter Loan ID to calculate EMI: ");
                        double emi = repo.CalculateEMI(int.Parse(Console.ReadLine()));
                        Console.WriteLine($"EMI: {emi}");
                        break;

                    case "6":
                        Console.Write("Enter Loan ID: ");
                        int loanId = int.Parse(Console.ReadLine());
                        Console.Write("Enter amount to repay: ");
                        double amount = double.Parse(Console.ReadLine());
                        repo.LoanRepayment(loanId, amount);
                        break;

                    case "7":
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid Option");
                        break;
                }
            }
        }
    }
}
