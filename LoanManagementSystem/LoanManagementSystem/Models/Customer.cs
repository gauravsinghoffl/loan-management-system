using System;

namespace Models
{
    /// <summary>
    /// Represents a loan customer with personal and credit information.
    /// </summary>
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int CreditScore { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Customer() { }

        /// <summary>
        /// Overloaded constructor to initialize all properties.
        /// </summary>
        public Customer(int customerId, string name, string emailAddress, string phoneNumber, string address, int creditScore)
        {
            CustomerId = customerId;
            Name = name;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
            Address = address;
            CreditScore = creditScore;
        }

        /// <summary>
        /// Returns a string representation of the customer.
        /// </summary>
        public override string ToString()
        {
            return $"CustomerId: {CustomerId}, Name: {Name}, Email: {EmailAddress}, Phone: {PhoneNumber}, Address: {Address}, Credit Score: {CreditScore}";
        }
    }
}
