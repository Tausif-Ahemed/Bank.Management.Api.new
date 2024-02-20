using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using static Bank.Management.Api.Helper.TransactionHelper;

namespace Bank.Management.Api.Models
{
    [Table("Transactions")]
    public class Transaction
    {
    
        public int Id { get; set; }
        public string TransactionUniqueReference { get; set; } //this will be generated in every instance of this class (i have provided it in ctor below)
        public decimal TransactionAmount { get; set; }
        public TranStatus TransactionStatus { get; set; } //this enum will come from TransactionHelper
        public bool IsSuccessfull => TransactionStatus.Equals(TranStatus.Success);
        public string TransactionSourceAccount { get; set; }
        public string TransactionDestinationAccount { get; set; }
        public string TransactionPerticulars { get; set; }
        public TranType TransactionType { get; set; } //this enum will come from TransactionHelper
        public DateTime TransactionDate { get; set; }

        public Transaction()
        {
               TransactionUniqueReference = $"{Guid.NewGuid().ToString().Replace("-" , "").Substring(1,27)}";
               //we used Guid to generate it 
        }
    }
    
}