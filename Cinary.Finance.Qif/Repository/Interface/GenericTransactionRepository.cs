using Cinary.Finance.Qif.Transaction;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Cinary.Finance.Qif.Repository
{
    public abstract class GenericTransactionRepository<TTransaction> : AbstractTransactionRepository
    where TTransaction : class, ITransaction
    {
        public async override Task<ITransactionDetail> ResolveAsync(string id, Stream resource)
        {
            var list = await _reader.ReadAsync<TTransaction>(resource);
            return OnCreate(id, list);
        }

        public override ITransactionDetail Resolve(string id, Stream resource)
        {
            var list = _reader.Read<TTransaction>(resource);
            return OnCreate(id, list);
        }

        public async override Task<ITransactionDetail> ResolveFromResourceAsync(string path)
        {
            ITransactionDetail result = default(ITransactionDetail);
            if (_container.ContainsKey(path))
            {
                result = _container[path];
            }
            else
            {
                var transactions = await _reader.ReadFromResourceAsync<TTransaction>(path);
                result = OnCreate(path, transactions);
            }
            return result;
        }

        public override ITransactionDetail ResolveFromResource(string path)
        {
            ITransactionDetail result = default(ITransactionDetail);
            if (_container.ContainsKey(path))
            {
                result = _container[path];
            }
            else
            {
                var transactions = _reader.ReadFromResource<TTransaction>(path);
                result = OnCreate(path, transactions);
            }
            return result;
        }

        public async override Task<ITransactionDetail> ResolveFromFileAsync(string path)
        {
            ITransactionDetail result = default(ITransactionDetail);
            if (_container.ContainsKey(path))
            {
                result = _container[path];
            }
            else
            {
                var transactions = await _reader.ReadFromFileAsync<TTransaction>(path);
                result = OnCreate(path, transactions);
            }
            return result;
        }

        public override ITransactionDetail ResolveFromFile(string path)
        {
            ITransactionDetail result = default(ITransactionDetail);
            if (_container.ContainsKey(path))
            {
                result = _container[path];
            }
            else
            {
                var transactions = _reader.ReadFromFile<TTransaction>(path);
                result = OnCreate(path, transactions);
            }
            return result;
        }

        public void Dispose()
        {
        }

        protected abstract ITransactionDetail OnCreate(string id, IList<TTransaction> transactions);
    }
}
