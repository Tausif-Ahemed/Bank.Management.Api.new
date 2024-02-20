using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Bank.Management.Api.Services.Interfaces
{
    public interface ITransactionService
    {
        Response CreateNewTransaction(Transaction transaction);
        Response FindTransactionByDate(DateTime date);
        Response MakeDeposite(string AccountNumber, decimal Amount,int TrasactionPin);
        Response MakeWithdrawal(string AccountNumber,decimal Amount, int TransactionPin);
        Response MakeFundTransfer(string FromAccount, string ToAccount, decimal Amount, string TransactionPin);

    }
}