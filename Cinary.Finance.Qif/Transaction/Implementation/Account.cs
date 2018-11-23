using System.Collections.Generic;

namespace Cinary.Finance.Qif.Transaction
{
    public class Account : IAccount
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public IList<ITransactionGroup> Transactions { get; set; }

        public Account()
        {
            Transactions = new List<ITransactionGroup>();
        }
    }
}
