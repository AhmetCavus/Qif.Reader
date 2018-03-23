using System.Collections.Generic;

namespace Cinary.Finance.Qif.Transaction
{
    public class TransactionModel : ITransactionDetail
    {
        public string Type { get; set; }

        public long TimeStamp { get; set; }
        public IList<ICategory> Categories { get; set; }

        public TransactionModel()
        {
            Categories = new List<ICategory>();
        }
    }
}