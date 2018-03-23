using Cinary.Finance.Qif.Data;
using System;

namespace Cinary.Finance.Qif.Repository
{
    public interface ITransactionRepositoryContainer : IDisposable
    {
        AbstractTransactionRepository Resolve(Type transactionType);
        AbstractTransactionRepository Resolve<TTarget>() where TTarget : ITransaction;
    }
}
