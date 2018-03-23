using System.Collections.Generic;

namespace Cinary.Finance.Qif.Dto
{
    public class TransactionDto<TTransaction> where TTransaction : ITransaction
    {
        public long TimeStamp { get; set; }
        public IList<TTransaction> Transactions { get; set; }
    }
}
