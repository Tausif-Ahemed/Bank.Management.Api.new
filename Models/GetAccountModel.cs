using System.ComponentModel.DataAnnotations;
using static Bank.Management.Api.Helper.AccountHelper;

namespace Bank.Management.Api;

public class GetAccountModel
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
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

}
