using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Management.Api.Models
{
    public class AuthenticateModel
    {
        [Required]
        [RegularExpression(@"^[0][1-9]\d{9}$|^[1-9]\d{9}$")] //here we are checking wheather the account numbre is 10 digit or not
        public string AccountNumber { get; set; }
         [Required]
        public string Pin { get; set; }

    }
}