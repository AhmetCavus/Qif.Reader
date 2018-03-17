using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Cinary.Finance.Qif.Data.Model
{
    public class QifModel : ITransaction
    {
        public string Type { get; set; }

        public long TimeStamp { get; set; }
        public List<Category> Categories { get; set; }

        public QifModel()
        {
            Categories = new List<Category>();
        }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        //public List<Account> Accounts { get; set; }
        //public List<Transaction> Transactions { get; set; }
        public List<TransactionGroup> TransactionGroups { get; set; }

        public Category()
        {
            //Accounts = new List<Account>();
            //Transactions = new List<Transaction>();
            TransactionGroups = new List<TransactionGroup>();
        }
    }

    public class Account
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Transaction> Transactions { get; set; }

        public Account()
        {
            Transactions = new List<Transaction>();
        }
    }

    public class Transaction
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

    public class TransactionGroup : ObservableCollection<Transaction>
    {
        public string Title { get; set; }
        public string Detail { get; set; }
        public char ShortName { get; set; }
    }

}