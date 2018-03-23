using System;

namespace Cinary.Finance.Qif.NonTransactionTypes
{
    public class Amortization
    {
        public DateTime FirstPaymentDate { get; set; }
        public int TotalYearsForLoan { get; set; }
        public int NoOfPaymentsPaid { get; set; }
        public int NoOfPeriodsPerYear { get; set; }
        public decimal InterestRate { get; set; }
        public decimal CurrentLoanBalance { get; set; }
        public decimal OriginalLoadAmount { get; set; }
    }
}
