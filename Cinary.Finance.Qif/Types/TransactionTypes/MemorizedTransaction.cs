using Cinary.Finance.Qif.NonTransactionTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cinary.Finance.Qif.TransactionTypes
{
    public class MemorizedTransaction : ITransaction
    {
        public MemorizedTransaction()
        {
            this.Payee = new List<string>();
            this.AddressLines = new List<string>();
        }
        public MemorizedType MemorizedType { get; set; }
        public decimal Amount { get; set; }
        public bool IsCleared { get; set; }
        public IList<string> Payee { get; set; }
        public string Memo { get; set; }
        public IList<string> AddressLines { get; set; }
        public string Category { get; set; }
        public Amortization Amortization { get; set; }
        public string Type { get; set; }
        public string AddressLine { get { return string.Join(" ", AddressLines.ToArray()); } }
        public string PayeeLine { get { return string.Join(" ", Payee.ToArray()); } }
        public DateTime Date { get; set; }

    }
}
