using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static Bank.Management.Api.Helper.AccountHelper;

namespace Bank.Management.Api.Models
{
    [Table("Accounts")]
    public class Account
    {

        public int Id { get; set; }
        public string FirstName { get; set;}
        public string LastName { get; set; }
        public string AccountName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public decimal CurrentBalance { get; set; }
        public AccountType AccountType { get; set; } //this will be enum which will return type of account
        
        public string AccountNumberGenerated { get; set; } //this will come from constructor

        // we are gonna save hash and salt for transaction pin
        public byte[] PinHash { get; set; }
        public byte[] PinSalt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        //constructor for accountGeneration
        Random randomNumber = new Random();
        public Account()
        {
            AccountNumberGenerated = Convert.ToString((long) randomNumber.NextDouble() * 9_000_000_000L + 1_000_000_000L);

            AccountName = $"{FirstName} + {LastName}"; //Account name will be Concatination of fname and lname 

        }
    }

}