using System;
using System.Collections.Generic;

namespace Cinary.Finance.Qif.Transaction
{
    public interface ITransactionEntry
    {
        int Id { get; set; }
        DateTime Date { get; set; }
        decimal Amount { get; set; }
        IList<decimal> SplitAmount { get; set; }
        IList<decimal> SplitAmounts { get; set; }
        bool IsCleared { get; set; }
        string Num { get; set; }
        IList<string> PayeeLines { get; set; }
        string Memo { get; set; }
        IList<string> SplitMemo { get; set; }
        IList<string> AddressLines { get; set; }
        string Category { get; set; }
        IList<string> SplitCategory { get; set; }
        string Type { get; set; }
        string Address { get; }
        string Payee { get; }
        string PayeeAccount { get; }
        string PayeeName { get; }
    }
}
