using System;
using System.Collections.Generic;

namespace Cinary.Finance.Qif.Transaction
{
    public class TransactionEntry : ITransactionEntry
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public IList<decimal> SplitAmount { get; set; }
        public IList<decimal> SplitAmounts { get; set; }
        public bool IsCleared { get; set; }
        public string Num { get; set; }
        public IList<string> PayeeLines { get; set; }
        public string Memo { get; set; }
        public IList<string> SplitMemo { get; set; }
        public IList<string> AddressLines { get; set; }
        public string Category { get; set; }
        public IList<string> SplitCategory { get; set; }
        public string Type { get; set; }
        public string Address { get; set; }
        public string Payee { get; set; }
        public string PayeeAccount { get; set; }
        public string PayeeName { get; set; }
    }
}
