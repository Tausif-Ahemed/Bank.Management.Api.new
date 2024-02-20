using System.ComponentModel.DataAnnotations;

namespace Bank.Management.Api;

public class UpdateAccountModel
{

        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]/d{4}$", ErrorMessage = "Pin must not be more than 4 Digits")] //it will be a 4-digit string
        public string Pin { get; set; }
        [Required]
        [Compare("Pin" ,ErrorMessage = "Pin Do not Match")] //we want to compare both of them
        public string ConfirmPin { get; set; }
        public DateTime UpdatedAt { get; set; }
}
