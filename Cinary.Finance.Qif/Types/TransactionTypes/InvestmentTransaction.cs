using System;

namespace Cinary.Finance.Qif.TransactionTypes
{
    public class InvestmentTransaction : ITransaction
    {
        public DateTime Date { get; set; }
        public string Action { get; set; }
        public string Security { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal TransactionAmount { get; set; }
        public bool IsCleared { get; set; }
        public string Reminder { get; set; }
        public string Memo { get; set; }
        public decimal Commission { get; set; }
        public string AccountForTransfer { get; set; }
        public decimal TransferredAmount { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
    }
}
