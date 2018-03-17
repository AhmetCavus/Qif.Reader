using Cinary.Finance.Qif.Data;
using Cinary.Finance.Qif.Data.Description;
using Cinary.Finance.Qif.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinary.Finance.Qif.Repository
{
    public abstract class AbstractQifRepository<TTarget> : ITransactionRepository<TTarget>
    where TTarget : ITransaction
    {
        protected Service.ITransactionReader _qif;
        protected Dictionary<string, TTarget> _container = new Dictionary<string, TTarget>();

        public abstract Task<TTarget> ResolveAsync(IDataDescription description = null);
        public abstract TTarget Resolve(IDataDescription description = null);
        public abstract Task<ITransaction> ResolveAsync(string resource);
        public abstract ITransaction Resolve(string resource);

        public void SetTransactionReader(ITransactionReader context) => _qif = context;

        public void Dispose()
        {
        }
    }
}
