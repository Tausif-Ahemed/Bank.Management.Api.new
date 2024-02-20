using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Management.Api.Helper
{
    public class TransactionHelper
    {
        public enum TranStatus{
        Success,
        Failed,
        Error
    }
    public enum TranType{
        Deposite,
        Withdrawal,
        Transfer
    }
    }
}