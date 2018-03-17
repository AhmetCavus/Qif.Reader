using Cinary.Finance.Qif.Data.TransactionTypes;
using System.Collections.Generic;

namespace Cinary.Finance.Qif.Data.Dto
{
    public class TransactionDto
    {
        public long TimeStamp { get; set; }
        public IList<NonInvestmentTransaction> Transactions { get; set; }
    }
}
