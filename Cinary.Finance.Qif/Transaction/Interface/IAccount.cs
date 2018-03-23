﻿using System.Collections.Generic;

namespace Cinary.Finance.Qif.Transaction
{
    public interface IAccount
    {
        string Name { get; set; }
        string Address { get; set; }
        IList<ITransactionDetail> Transactions { get; set; }
    }
}
