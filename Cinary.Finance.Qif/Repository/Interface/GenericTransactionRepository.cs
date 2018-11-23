using Cinary.Finance.Qif.Transaction;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Cinary.Finance.Qif.Repository
{
    public abstract class GenericTransactionRepository<TTransaction> : AbstractTransactionRepository
    where TTransaction : class, ITransactionEntry
    {
        public async override Task<IList<ITransactionEntry>> ResolveAsync(string id, Stream resource)
        {
            return await _reader.ReadAsync<TTransaction>(resource);
        }

        public override IList<ITransactionEntry> Resolve(string id, Stream resource)
        {
            return _reader.Read<TTransaction>(resource);
        }

        public async override Task<IList<ITransactionEntry>> ResolveFromResourceAsync(string path)
        {
            IList<ITransactionEntry> result = default(IList<ITransactionEntry>);
            if (_container.ContainsKey(path))
            {
                result = _container[path];
            }
            else
            {
                result = await _reader.ReadFromResourceAsync<TTransaction>(path);
            }
            return result;
        }

        public override IList<ITransactionEntry> ResolveFromResource(string path)
        {
            IList<ITransactionEntry> result = default(IList<ITransactionEntry>);
            if (_container.ContainsKey(path))
            {
                result = _container[path];
            }
            else
            {
                result = _reader.ReadFromResource<TTransaction>(path);
            }
            return result;
        }

        public async override Task<IList<ITransactionEntry>> ResolveFromFileAsync(string path)
        {
            IList<ITransactionEntry> result = default(IList<ITransactionEntry>);
            if (_container.ContainsKey(path))
            {
                result = _container[path];
            }
            else
            {
                result = await _reader.ReadFromFileAsync<TTransaction>(path);
            }
            return result;
        }

        public override IList<ITransactionEntry> ResolveFromFile(string path)
        {
            IList<ITransactionEntry> result = default(IList<ITransactionEntry>);
            if (_container.ContainsKey(path))
            {
                result = _container[path];
            }
            else
            {
                result = _reader.ReadFromFile<TTransaction>(path);
            }
            return result;
        }

        public void Dispose()
        {
        }

    }
}
