using System.Collections.Generic;

namespace Cinary.Finance.Qif.Transaction
{
    public class Category : ICategory
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        //public List<Account> Accounts { get; set; }
        //public List<Transaction> Transactions { get; set; }
        public IList<AbstractTransactionEntryGroup> TransactionGroups { get; set; }

        public Category()
        {
            //Accounts = new List<Account>();
            //Transactions = new List<Transaction>();
            TransactionGroups = new List<AbstractTransactionEntryGroup>();
        }
    }
}
