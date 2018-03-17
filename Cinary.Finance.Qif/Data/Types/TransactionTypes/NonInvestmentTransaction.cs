using System;
using System.Collections.Generic;
using System.Linq;

namespace Cinary.Finance.Qif.Data.TransactionTypes
{
    public class NonInvestmentTransaction : ITransaction
    {
        public NonInvestmentTransaction()
        {
            this.PayeeLines = new List<string>();
            this.AddressLines = new List<string>();
            this.SplitMemo = new List<string>(); ;
            this.SplitCategory = new List<string>(); ;
            this.SplitAmount = new List<decimal>();
        }
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
        public string Address { get { return string.Join(" ", AddressLines.ToArray()); } }
        //public string Payee { get { return string.Join(" ", Payee); } }
        public string Payee { get; }
        public string PayeeAccount { get { return PayeeLines.Count>0? PayeeLines[0]:string.Empty;} }
        public string PayeeName { get { return PayeeLines.Count > 1 ? PayeeLines[1] : string.Empty; } }
    }
}
