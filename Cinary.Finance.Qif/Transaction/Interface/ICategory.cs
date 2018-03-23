using System.Collections.Generic;

namespace Cinary.Finance.Qif.Transaction
{
    public interface ICategory
    {
        int Id { get; set; }
        string Type { get; set; }
        string Title { get; set; }
        string Desc { get; set; }
        //public List<Account> Accounts { get; set; }
        //public List<Transaction> Transactions { get; set; }
        IList<AbstractTransactionEntryGroup> TransactionGroups { get; set; }
    }
}
