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
        protected Dictionary<string, IList<ITransactionEntry>> _container = new Dictionary<string, IList<ITransactionEntry>>();

        public abstract IList<ITransactionEntry> Resolve(string id, Stream resource);
        public abstract Task<IList<ITransactionEntry>> ResolveAsync(string id, Stream resource);
        public abstract IList<ITransactionEntry> ResolveFromFile(string path);
        public abstract Task<IList<ITransactionEntry>> ResolveFromFileAsync(string path);
        public abstract IList<ITransactionEntry> ResolveFromResource(string path);
        public abstract Task<IList<ITransactionEntry>> ResolveFromResourceAsync(string path);

        public void SetTransactionReader(ITransactionReader reader) => _reader = reader;

    }
}
