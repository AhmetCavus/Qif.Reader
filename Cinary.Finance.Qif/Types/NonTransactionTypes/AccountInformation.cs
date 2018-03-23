using System;

namespace Cinary.Finance.Qif.NonTransactionTypes
{
    public class AccountInformation
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal CreditLimit { get; set; }
        public DateTime BalanceDate { get; set; }
        public decimal BalanceAmount { get; set; }
    }
}
