using System.Collections.Generic;

namespace Cinary.Finance.Qif.Transaction
{
    public interface ITransactionDetail : ITransaction
    {
        long TimeStamp { get; set; }
        IList<ICategory> Categories { get; set; }
    }
}