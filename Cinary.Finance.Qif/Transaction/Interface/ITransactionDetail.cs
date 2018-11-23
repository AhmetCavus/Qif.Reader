using System.Collections.Generic;

namespace Cinary.Finance.Qif.Transaction
{
    public interface ITransactionGroup : ITransactionEntry
    {
        long TimeStamp { get; set; }
        IList<ICategory> Categories { get; set; }
    }
}