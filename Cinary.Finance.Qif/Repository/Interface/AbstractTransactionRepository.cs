using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Cinary.Finance.Qif.Service;
using Cinary.Finance.Qif.Transaction;

namespace Cinary.Finance.Qif.Repository
{
    public abstract class AbstractTransactionRepository : ITransactionStreamRepository, ITransactionResourceRepository, ITransactionFileRepository
    {
        protected ITransactionReader _reader;
        protected Dictionary<string, ITransactionDetail> _container = new Dictionary<string, ITransactionDetail>();

        public abstract ITransactionDetail Resolve(string id, Stream resource);
        public abstract Task<ITransactionDetail> ResolveAsync(string id, Stream resource);
        public abstract ITransactionDetail ResolveFromFile(string path);
        public abstract Task<ITransactionDetail> ResolveFromFileAsync(string path);
        public abstract ITransactionDetail ResolveFromResource(string path);
        public abstract Task<ITransactionDetail> ResolveFromResourceAsync(string path);

        public void SetTransactionReader(ITransactionReader reader) => _reader = reader;

    }
}
