
# Loan Management System

A console-based loan application developed using C# and .NET Framework 4.7.2 as part of the Hexaware Coding Challenge. The system handles loan applications, EMI and interest calculation, loan approval based on credit score, and loan repayments.

## Problem Statement

Create SQL Schema from the `Customer` and `Loan` classes using the class attributes as table columns.

1. Define a `Customer` class with the following confidential attributes:
   - Customer ID
   - Name
   - Email Address
   - Phone Number
   - Address
   - Credit Score

2. Define a base class `Loan` with the following attributes:
   - Loan ID
   - Customer (reference to Customer class)
   - Principal Amount
   - Interest Rate
   - Loan Term (Loan Tenure in months)
   - Loan Type (CarLoan, HomeLoan)
   - Loan Status (Pending, Approved)

## Features

- Apply for car and home loans.
- Store customer and loan details in SQL Server.
- Approve or reject loan based on credit score.
- Calculate interest and EMI based on formulas.
- View loans, retrieve by ID, and handle repayments.
- Handle exceptions using custom exception classes.
- Menu-driven interface for all functionalities.

## Technologies Used

- **Language**: C# (.NET Framework 4.7.2)
- **IDE**: Visual Studio 2022
- **Database**: SQL Server (LocalDB)
- **Data Access**: ADO.NET, System.Configuration

## Project Structure

📦 LoanManagementSystem/  
├── 📁 Models/             → Entity classes: Customer, Loan, CarLoan, HomeLoan  
├── 📁 DAO/                → Interfaces and implementations (ILoanRepository, LoanRepositoryImpl)  
├── 📁 Data/               → DB utilities (DBConnUtil, DBPropertyUtil)  
├── 📁 Exceptions/         → Custom exceptions (InvalidLoanException)  
├── 📁 Main/               → Entry point (MainModule.cs - menu-driven console UI)  
├── ⚙️  App.config         → Connection string configuration  
└── 📝 LoanDB.sql          → SQL schema script (optional)  



## Key Learnings

- Real-world object-oriented design principles
- Use of ADO.NET for direct database interaction
- Exception handling with custom exceptions
- Inheritance and abstraction through class hierarchies
- Console-based user interaction and control flow
