using Cinary.Finance.Qif.Transaction;
using System.Collections.Generic;

namespace Cinary.Finance.Qif.Dto
{
    public class TransactionDto<TTransaction> where TTransaction : ITransactionEntry
    {
        public long TimeStamp { get; set; }
        public IList<TTransaction> Transactions { get; set; }
    }
}
