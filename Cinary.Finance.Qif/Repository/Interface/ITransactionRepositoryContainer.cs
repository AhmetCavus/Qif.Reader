using Cinary.Finance.Qif.Data;
using System;

namespace Cinary.Finance.Qif.Repository
{
    public interface ITransactionRepositoryContainer : IDisposable
    {
        ITransactionRepository Resolve(Type transactionType);
        ITransactionRepository<TTarget> Resolve<TTarget>() where TTarget : ITransaction;
    }
}
