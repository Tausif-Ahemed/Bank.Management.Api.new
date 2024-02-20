using System.ComponentModel.DataAnnotations;
using static Bank.Management.Api.Helper.AccountHelper;

namespace Bank.Management.Api;

public class RegisterNewAccountModel
{
    //Besically it will have maximum  fields of account model except some fields
       
        public string FirstName { get; set;}
        public string LastName { get; set; }
      //public string AccountName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        //public decimal CurrentBalance { get; set; }
        public AccountType AccountType { get; set; } //this will be enum which will return type of account
        
        //public string AccountNumberGenerated { get; set; } //this will come from constructor

        // we are gonna save hash and salt for transaction pin
        //public byte[] PinHash { get; set; }
        //public byte[] PinSalt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        //so let's Create Regular Expression
        [Required]
        [RegularExpression(@"^[0-9]\d{4}$", ErrorMessage = "Pin must not be more than 4 Digits")] //it will be a 4-digit string
        public string Pin { get; set; }
        [Required]
        [Compare("Pin" , ErrorMessage = "Pin Do not Match")]
        public string ConfirmPin { get; set; }
}
